using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Stats
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private float experiencePoints;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
        }
    }
}