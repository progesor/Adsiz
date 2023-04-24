using System;
using ProgesorCreating.Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI AIText;
        [SerializeField] private Button nextButton;
        [SerializeField] private Transform choiceRoot;
        [SerializeField] private GameObject choicePrefab;
        
        private PlayerConversant _playerConversant;

        private void Start()
        {
            _playerConversant = GameObject.FindWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(Next);
            
            UpdateUI();
        }

        void Next()
        {
            _playerConversant.Next();
            UpdateUI();
        }

        private void UpdateUI()
        {
            AIText.SetText(_playerConversant.GetText());
            nextButton.gameObject.SetActive(_playerConversant.HasNext());

            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }

            foreach (string choiceText in _playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choiceText;
            }
        }
    }
}