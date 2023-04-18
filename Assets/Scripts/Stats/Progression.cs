using System;
using UnityEngine;

namespace ProgesorCreating.RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression",menuName = "Stats/New Progression",order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] characterClass = null;
        
        [Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] private CharacterClass characterClass;
            [SerializeField] private float[] health;
        }
    }
}