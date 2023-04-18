using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression",menuName = "Stats/New Progression",order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] characterClasses;

        public float GetStats(Stat stat, CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.characterClass != characterClass) continue;

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    if (progressionStat.stat != stat) continue;

                    if (progressionStat.levels.Length < level) continue;

                    return progressionStat.levels[level - 1];
                }
            }
            return 0;
        }
    }
}