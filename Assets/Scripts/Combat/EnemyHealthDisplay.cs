using ProgesorCreating.Attributes;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Combat
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

            _textMeshProUGUI.SetText(showPercentage
                ? $"{_health.GetPercentage():0.0}%"
                : $"{_health.GetHealthPoint():0}/{_health.GetMaxHealthPoint():0}");
        }
    }
}