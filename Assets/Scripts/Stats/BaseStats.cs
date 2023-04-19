﻿using System;
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
        [SerializeField] private GameObject levelUpParticleEffect;
        [SerializeField] private bool shouldUseModifiers;

        public event Action OnLevelUp;

        private int _currentLevel;
        private Experience _experience;

        private void Awake()
        {
            _experience = GetComponent<Experience>();
        }

        private void Start()
        {
            _currentLevel = CalculateLevel();
        }

        private void OnEnable()
        {
            if (_experience!=null)
            {
                _experience.OnExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (_experience!=null)
            {
                _experience.OnExperienceGained -= UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel>_currentLevel)
            {
                _currentLevel = newLevel;
                LevelUpEffect();
                OnLevelUp?.Invoke();
            }
        }
        
        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat) / 100);
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, _currentLevel);
        }

        public int GetLevel()
        {
            if (_currentLevel<1)
            {
                _currentLevel = CalculateLevel();
            }
            return _currentLevel;
        }
        
        private float GetAdditiveModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;
            
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }

            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;
            
            float total = 0;
            
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            
            return total;
        }

        private int CalculateLevel()
        {
            if (_experience == null) return startingLevel;
            
            float currentXp = _experience.GetPoints();
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