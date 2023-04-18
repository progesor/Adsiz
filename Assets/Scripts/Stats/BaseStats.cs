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

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, startingLevel);
        }

        public float GetExperinceReward()
        {
            return 10;
        }
    }
}