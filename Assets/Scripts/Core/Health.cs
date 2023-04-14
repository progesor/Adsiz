using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;

        private bool _isDead;
        private static readonly int Die1 = Animator.StringToHash("die");

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
    }
}