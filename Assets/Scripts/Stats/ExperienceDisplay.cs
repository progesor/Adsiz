using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        private Experience _experience;
        private TextMeshProUGUI _textMeshProUGUI;

        private void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            _textMeshProUGUI.SetText(String.Format("{0:0}",_experience.GetPoints()));
        }
    }
}