using System;
using ProgesorCreating.Saving;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Stats
{
    public class Experience : MonoBehaviour,ISaveable
    {
        [SerializeField] private float experiencePoints;

        public event Action OnExperienceGained;

        private void Update()
        {
            if (Input.GetKey(KeyCode.End))
            {
                GainExperience(Time.deltaTime * 1000);
            }
        }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            OnExperienceGained?.Invoke();
        }
        
        public float GetPoints()
        {
            return experiencePoints;
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}