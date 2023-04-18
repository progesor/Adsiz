using System;
using ProgesorCreating.RPG.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        private Fighter _fighter;
        
        private Health _health;

        private void Awake()
        {
            _fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if (_fighter.GetTarget()==null)
            {
                GetComponent<TextMeshProUGUI>().SetText("N/A");
            }
            else
            {
                _health = _fighter.GetTarget();
                GetComponent<TextMeshProUGUI>().SetText(String.Format("{0:0.0}%",_health.GetPercentage()));
            }
        }
    }
}