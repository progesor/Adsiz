using ProgesorCreating.RPG.Core;
using ProgesorCreating.RPG.Movement;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 0.8f;
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private Transform handTransform;
        [SerializeField] private Weapon weapon;

        private Health _target;
        private float _timeSinceLastAttack = Mathf.Infinity;
        private Mover _mover;
        private static readonly int Attack1 = Animator.StringToHash("attack");
        private static readonly int StopAttack1 = Animator.StringToHash("stopAttack");
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
            SpawnWeapon();
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

        private void SpawnWeapon()
        {
            if (weapon == null) return;
            weapon.Spawn(handTransform, _animator);
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
            if (_target==null)return;
            _target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weaponRange;
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
            return weaponRange;
        }
    }
}
