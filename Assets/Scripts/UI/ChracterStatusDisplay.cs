using System;
using ProgesorCreating.RPG.Attributes;
using ProgesorCreating.RPG.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.UI
{
    public class ChracterStatusDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LevelText;
        [SerializeField] private Slider HealthBar;
        
        private Health _health;
        private BaseStats _baseStats;

        private void Awake()
        {
            _health = GameObject.FindWithTag("Player").GetComponent<Health>();
            _baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            HealthBar.maxValue = _health.GetMaxHealthPoint();
            HealthBar.value = _health.GetHealthPoint();
            LevelText.SetText(_baseStats.GetLevel().ToString());
        }
    }
}