using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Stats
{
    public class DamageDisplay : MonoBehaviour
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
            _textMeshProUGUI.SetText(String.Format("{0:0.0}/{1:0.0}/{2:0.0}/{3:0.0}",_baseStats.GetBaseStat(Stat.Damage),_baseStats.GetAdditiveModifier(Stat.Damage),_baseStats.GetPercentageModifier(Stat.Damage),_baseStats.GetStat(Stat.Damage)));
        }
    }
}