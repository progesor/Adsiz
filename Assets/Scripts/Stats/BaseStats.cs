using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression;

        private int _currentLevel;

        private void Start()
        {
            _currentLevel = CalculateLevel();
        }

        private void Update()
        {
            int newLevel = CalculateLevel();
            if (newLevel>_currentLevel)
            {
                _currentLevel = newLevel;
            }
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }

        public int GetLevel()
        {
            return _currentLevel;
        }

        public int CalculateLevel()
        {
            Experience experience= GetComponent<Experience>();
            if (experience == null) return startingLevel;
            
            float currentXp = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float xpToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (xpToLevelUp > currentXp)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}