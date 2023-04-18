using ProgesorCreating.RPG.Attributes;
using ProgesorCreating.RPG.Core;
using ProgesorCreating.RPG.Movement;
using ProgesorCreating.RPG.Saving;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction, ISaveable
    {
        [SerializeField] private float timeBetweenAttacks = 0.8f;
        [SerializeField] private Transform rightHandTransform;
        [SerializeField] private Transform leftHandTransform;
        [SerializeField] private Weapon defaultWeapon;

        private Health _target;
        private float _timeSinceLastAttack = Mathf.Infinity;
        private Weapon _currentWeapon;
        private Mover _mover;
        private static readonly int Attack1 = Animator.StringToHash("attack");
        private static readonly int StopAttack1 = Animator.StringToHash("stopAttack");
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
            
            if (_currentWeapon==null)
            {
                EquipWeapon(defaultWeapon);
            }
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_target==null)return;
            if (_target.IsDead())return;
            
            if (_target!=null && !GetIsInRange())
            {
                _mover.MoveTo(_target.transform.position, 1f);
            }
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;
            if (weapon!=null)
            {
                weapon.Spawn(rightHandTransform,leftHandTransform, _animator);
            }
        }

        public Health GetTarget()
        {
            return _target;
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

        private void TriggerAttack()
        {
            _animator.ResetTrigger(StopAttack1);
            _animator.SetTrigger(Attack1);
        }

        public void Hit()
        {
            if (_target == null) return;

            if (_currentWeapon.HasProjectile())
            {
                _currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, _target, gameObject);
            }
            else
            {
                _target.TakeDamage(gameObject,_currentWeapon.GetDamage());
            }
        }

        public void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < _currentWeapon.GetRange();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget==null)
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
            if (_currentWeapon!=null)
            {
                return _currentWeapon.GetRange();
            }

            return defaultWeapon.GetRange();
        }

        public object CaptureState()
        {
            return _currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}
