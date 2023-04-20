using ProgesorCreating.RPG.Core;
using ProgesorCreating.RPG.Saving;
using ProgesorCreating.RPG.Stats;
using ProgesorCreating.RPG.Utils;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Attributes
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] private float regenerationPercentage = 70f;
        [SerializeField] private UnityEvent takeDamage;
        
        private LazyValue<float> _healthPoints;

        private bool _isDead;
        private BaseStats _baseStats;
        private static readonly int Die1 = Animator.StringToHash("die");

        private void Awake()
        {
            _baseStats = GetComponent<BaseStats>();
            _healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return _baseStats.GetStat(Stat.Health);
        }

        private void Start()
        {
            _healthPoints.ForceInit();
        }

        private void OnEnable()
        {
            _baseStats.OnLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            _baseStats.OnLevelUp -= RegenerateHealth;
        }


        public bool IsDead()
        {
            return _isDead;
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
            _healthPoints.value = Mathf.Max(_healthPoints.value - damage, 0);

            if (_healthPoints.value==0)
            {
               Die();
               AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke();
            }
        }

        public float GetPercentage()
        {
            return 100 * (_healthPoints.value / _baseStats.GetStat(Stat.Health));
        }

        public float GetHealthPoint()
        {
            return _healthPoints.value;
        }
        
        public float GetMaxHealthPoint()
        {
            return _baseStats.GetStat(Stat.Health);
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
            _healthPoints.value = Mathf.Max(_healthPoints.value, regenHealthPoints);
        }

        public object CaptureState()
        {
            return _healthPoints.value;
        }

        public void RestoreState(object state)
        {
            _healthPoints.value = (float)state;

            if (_healthPoints.value <= 0)
            {
                Die();
            }
        }
    }
}