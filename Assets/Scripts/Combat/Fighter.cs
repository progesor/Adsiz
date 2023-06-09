using System.Collections;
using System.Collections.Generic;
using ProgesorCreating.Attributes;
using ProgesorCreating.Core;
using ProgesorCreating.Inventories;
using ProgesorCreating.Movement;
using ProgesorCreating.Saving;
using ProgesorCreating.Stats;
using ProgesorCreating.Utils;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] private float timeBetweenAttacks = 0.8f;
        [SerializeField] private Transform rightHandTransform;
        [SerializeField] private Transform leftHandTransform;
        [SerializeField] private WeaponConfig defaultWeaponConfig;
        [SerializeField] private float autoAttackRange = 4f;

        private Equipment _equipment;
        private Health _target;
        private Mover _mover;
        private Animator _animator;
        private WeaponConfig _currentWeaponConfig;
        private float _timeSinceLastAttack = Mathf.Infinity;
        private static readonly int Attack1 = Animator.StringToHash("attack");
        private static readonly int StopAttack1 = Animator.StringToHash("stopAttack");
        private LazyValue<Weapon> _currentWeapon;
        

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
            _currentWeaponConfig = defaultWeaponConfig;
            _currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
            _equipment = GetComponent<Equipment>();
            if (_equipment)
            {
                _equipment.EquipmentUpdated += UpdateWeapon;
            }
        }


        private Weapon SetupDefaultWeapon()
        {
            return AttachWeapon(defaultWeaponConfig);
        }

        private void Start()
        {
            _currentWeapon.ForceInit();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_target==null)return;
            if (_target.IsDead())
            {
                _target = FindNewTargetInRange();
                if (_target==null)return;
            }
            
            if (_target!=null && !GetIsInRange(_target.transform))
            {
                _mover.MoveTo(_target.transform.position, 1f);
            }
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(WeaponConfig weaponConfig)
        {
            _currentWeaponConfig = weaponConfig;
            _currentWeapon.Value = AttachWeapon(weaponConfig);
        }

        private void UpdateWeapon()
        {
            var weapon = _equipment.GetItemInSlot(EquipLocation.Melee) as WeaponConfig;

            EquipWeapon(weapon == null ? defaultWeaponConfig : weapon);
        }
        
        private Weapon AttachWeapon(WeaponConfig weaponConfig)
        {
            return weaponConfig.Spawn(rightHandTransform, leftHandTransform, _animator);
        }

        public Health GetTarget()
        {
            return _target;
        }

        public Transform GetHandTransform(bool isRightHand)
        {
            if (isRightHand)
            {
                return rightHandTransform;
            }
            else
            {
                return leftHandTransform;
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            if (_timeSinceLastAttack>timeBetweenAttacks)
            {
                TriggerAttack();
                _timeSinceLastAttack = 0;
            }
            
        }

        private Health FindNewTargetInRange()
        {
            Health best = null;
            float bestDistance = Mathf.Infinity;
            foreach (Health candidate in FinAllAllTargetsInRange())
            {
                float candidateDistance = Vector3.Distance(transform.position, candidate.transform.position);
                if (candidateDistance<bestDistance)
                {
                    best = candidate;
                    bestDistance = candidateDistance;
                }
            }

            return best;
        }

        private IEnumerable<Health> FinAllAllTargetsInRange()
        {
            RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, autoAttackRange, Vector3.up);
            foreach (RaycastHit hit in raycastHits)
            {
                Health health = hit.transform.GetComponent<Health>();
                if (health==null)continue;
                if (health.IsDead())continue;
                if (health.gameObject==gameObject)continue;
                yield return health;
            }
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger(StopAttack1);
            _animator.SetTrigger(Attack1);
        }

        private void Hit()
        {
            if (_target == null) return;
            
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            BaseStats targetBaseStats = _target.GetComponent<BaseStats>();
            if (targetBaseStats)
            {
                float defence = targetBaseStats.GetStat(Stat.Defence);

                damage /= 1 + defence / damage;
            }

            if (_currentWeapon.Value!= null)
            {
                _currentWeapon.Value.OnHit();
            }
            
            if (_currentWeaponConfig.HasProjectile())
            {
                _currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, _target, gameObject,damage);
            }
            else
            {
                _target.TakeDamage(gameObject,damage);
            }
        }

        public void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) < _currentWeaponConfig.GetRange();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            if (!_mover.CanMoveTo(combatTarget.transform.position) && !GetIsInRange(combatTarget.transform))
            {
                return false;
            }

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            _target = null;
            _mover.Cancel();
        }

        private void StopAttack()
        {
            _animator.ResetTrigger(Attack1);
            _animator.SetTrigger(StopAttack1);
        }
        
        public float GetWeaponRange()
        {
            if (_currentWeaponConfig!=null)
            {
                return _currentWeaponConfig.GetRange();
            }

            return defaultWeaponConfig.GetRange();
        }

    }
}
