using ProgesorCreating.RPG.Core;
using ProgesorCreating.RPG.Saving;
using ProgesorCreating.RPG.Stats;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Attributes
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] private float regenerationPercentage = 70f;
        private float _healthPoints = -1f;

        private bool _isDead;
        private BaseStats _baseStats;
        private static readonly int Die1 = Animator.StringToHash("die");

        private void Awake()
        {
            _baseStats = GetComponent<BaseStats>();
            _baseStats.OnLevelUp += RegenerateHealth;
            
            if (_healthPoints<0)
            {
                _healthPoints = _baseStats.GetStat(Stat.Health);
            }
            
        }


        public bool IsDead()
        {
            return _isDead;
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
            _healthPoints = Mathf.Max(_healthPoints - damage, 0);
            if (_healthPoints==0)
            {
               Die();
               AwardExperience(instigator);
            }
        }

        public float GetPercentage()
        {
            return 100 * (_healthPoints / _baseStats.GetStat(Stat.Health));
        }
        
        private void Die()
        {
            if (_isDead)return;

            _isDead = true;
            GetComponent<Animator>().SetTrigger(Die1);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience==null)return;
            
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }
        
        private void RegenerateHealth()
        {
            float regenHealthPoints = _baseStats.GetStat(Stat.Health) * (regenerationPercentage / 100);
            _healthPoints = Mathf.Max(_healthPoints, regenHealthPoints);
        }

        public object CaptureState()
        {
            return _healthPoints;
        }

        public void RestoreState(object state)
        {
            _healthPoints = (float)state;

            if (_healthPoints <= 0)
            {
                Die();
            }
        }
    }
}