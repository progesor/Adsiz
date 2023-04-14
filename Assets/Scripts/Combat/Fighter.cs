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
        
        private Transform _target;
        private float _timeSinceLastAttack;
        private Mover _mover;
        private static readonly int Attack1 = Animator.StringToHash("attack");

        private void Start()
        {
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_target==null)return;
            
            if (_target!=null && !GetIsInRange())
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (_timeSinceLastAttack>timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger(Attack1);
                _timeSinceLastAttack = 0;
                
            }
            
        }
        
        public void Hit()
        {
            Health healthComponent = _target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.transform;
        }

        public void Cancel()
        {
            _target = null;
        }

    }
}
