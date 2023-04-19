using System;
using ProgesorCreating.RPG.Attributes;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        [SerializeField] private bool showPercentage;
        
        private Fighter _fighter;
        private Health _health;
        private TextMeshProUGUI _textMeshProUGUI;

        private void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if (_fighter.GetTarget() == null)
            {
                _textMeshProUGUI.SetText("N/A");
                return;
            }
            _health = _fighter.GetTarget();
            
            if (showPercentage)
            {
                _textMeshProUGUI.SetText(String.Format("{0:0.0}%", _health.GetPercentage()));
            }
            else
            {
                _textMeshProUGUI.SetText(String.Format("{0:0}/{1:0}", _health.GetHealthPoint(),_health.GetMaxHealthPoint()));
            }
            

        }
    }
}