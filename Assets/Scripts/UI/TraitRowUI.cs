using ProgesorCreating.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI
{
    public class TraitRowUI : MonoBehaviour
    {
        [SerializeField] private Trait trait;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private Button minusButton;
        [SerializeField] private Button plusButton;

        private int _value;

        private void Start()
        {
            minusButton.onClick.AddListener(()=>Allocate(-1));
            plusButton.onClick.AddListener(()=>Allocate(1));
        }

        private void Update()
        {
            minusButton.interactable = _value > 0;
            
            valueText.text = _value.ToString();
        }

        public void Allocate(int points)
        {
            _value += points;
            if (_value<0)
            {
                _value = 0;
            }
        }
    }
}