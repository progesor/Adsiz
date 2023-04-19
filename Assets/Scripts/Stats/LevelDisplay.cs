using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        private BaseStats _baseStats;
        private TextMeshProUGUI _textMeshProUGUI;

        private void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            _textMeshProUGUI.SetText(String.Format("{0:0}",_baseStats.GetLevel()));
        }
    }
}