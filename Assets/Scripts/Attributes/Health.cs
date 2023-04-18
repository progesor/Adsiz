using ProgesorCreating.RPG.Core;
using ProgesorCreating.RPG.Saving;
using ProgesorCreating.RPG.Stats;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Attributes
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] private float healthPoints = 100f;

        private bool _isDead;
        private static readonly int Die1 = Animator.StringToHash("die");

        private void Awake()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public bool IsDead()
        {
            return _isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints==0)
            {
               Die();
            }
        }

        private void Die()
        {
            if (_isDead)return;

            _isDead = true;
            GetComponent<Animator>().SetTrigger(Die1);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}