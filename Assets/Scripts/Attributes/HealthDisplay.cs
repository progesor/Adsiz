using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        
        
        private Health _health;

        private void Awake()
        {
            _health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            GetComponent<TextMeshProUGUI>().SetText(String.Format("{0:0.0}%",_health.GetPercentage()));
        }
    }
}