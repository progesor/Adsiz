using ProgesorCreating.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI unassignedPointsText;
        [SerializeField] private Button commitButton;
        
        private TraitStore _playerTraitStore;

        private void Start()
        {
            _playerTraitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();
            commitButton.onClick.AddListener(_playerTraitStore.Commit);
        }

        private void Update()
        {
            unassignedPointsText.text = _playerTraitStore.GetUnassignedPoints().ToString();
        }
    }
}