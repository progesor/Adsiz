using ProgesorCreating.Core;
using ProgesorCreating.Saving;
using ProgesorCreating.Stats;
using ProgesorCreating.Utils;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Attributes
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] private float regenerationPercentage = 70f;
        [SerializeField] private UnityEvent<float> takeDamage;
        [SerializeField] private UnityEvent onDie;
        
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
            _healthPoints.Value = Mathf.Max(_healthPoints.Value - damage, 0);

            if (_healthPoints.Value==0)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }
            takeDamage.Invoke(damage);
        }

        public void Heal(float healthToRestore)
        {
            _healthPoints.Value = Mathf.Min(_healthPoints.Value + healthToRestore, GetMaxHealthPoint());
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return _healthPoints.Value / _baseStats.GetStat(Stat.Health);
        }

        public float GetHealthPoint()
        {
            return _healthPoints.Value;
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
            _healthPoints.Value = Mathf.Max(_healthPoints.Value, regenHealthPoints);
        }

        public object CaptureState()
        {
            return _healthPoints.Value;
        }

        public void RestoreState(object state)
        {
            _healthPoints.Value = (float)state;

            if (_healthPoints.Value <= 0)
            {
                Die();
            }
        }
    }
}