using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression",menuName = "Stats/New Progression",order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] characterClasses;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.characterClass==characterClass)
                {
                    return progressionClass.health[level-1];
                }
            }
            return 0;
        }
    }
}