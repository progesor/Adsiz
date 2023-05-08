using System;
using ProgesorCreating.Attributes;
using ProgesorCreating.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI
{
    public class ChracterStatusDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LevelText;
        [SerializeField] private Slider HealthBar;
        [SerializeField] private Slider ManaBar;
        
        private Health _health;
        private BaseStats _baseStats;
        private Mana _mana;
        private GameObject _player;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _health = _player.GetComponent<Health>();
            _baseStats = _player.GetComponent<BaseStats>();
            _mana = _player.GetComponent<Mana>();
        }

        private void Update()
        {
            HealthBar.maxValue = _health.GetMaxHealthPoint();
            HealthBar.value = _health.GetHealthPoint();
            LevelText.SetText(_baseStats.GetLevel().ToString());
            ManaBar.maxValue = _mana.GetMaxMana();
            ManaBar.value = _mana.GetMana();
        }
    }
}