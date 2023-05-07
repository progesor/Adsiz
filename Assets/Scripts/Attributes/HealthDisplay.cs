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
            _textMeshProUGUI.SetText(showPercentage
                ? $"{_health.GetPercentage():0.0}%"
                : $"{_health.GetHealthPoint():0}/{_health.GetMaxHealthPoint():0}");
        }
    }
}