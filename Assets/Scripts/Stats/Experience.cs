﻿using System;
using ProgesorCreating.RPG.Saving;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Stats
{
    public class Experience : MonoBehaviour,ISaveable
    {
        [SerializeField] private float experiencePoints;

        public event Action OnExperienceGained;

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