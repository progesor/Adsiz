using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private bool showPercentage;
        
        private Health _health;
        private TextMeshProUGUI _textMeshProUGUI;

        private void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            if (showPercentage)
            {
                _textMeshProUGUI.SetText(String.Format("{0:0.0}%",_health.GetPercentage()));
            }
            else
            {
                _textMeshProUGUI.SetText(String.Format("{0:0}/{1:0}", _health.GetHealthPoint(),_health.GetMaxHealthPoint()));
            }
            
        }
    }
}