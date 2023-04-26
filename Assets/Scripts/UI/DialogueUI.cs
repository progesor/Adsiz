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
        [SerializeField] private GameObject AIResponse;
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
            AIResponse.SetActive(!_playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(_playerConversant.IsChoosing());
            if (_playerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                AIText.SetText(_playerConversant.GetText());
                nextButton.gameObject.SetActive(_playerConversant.HasNext());
            }
            
        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }

            foreach (DialogueNode choice in _playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choice.GetText();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    _playerConversant.SelectChoice(choice);
                    UpdateUI();
                });
            }
        }
    }
}