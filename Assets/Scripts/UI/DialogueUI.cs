using System;
using ProgesorCreating.Dialogue;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI AIText;
        
        private PlayerConversant _playerConversant;

        private void Start()
        {
            _playerConversant = GameObject.FindWithTag("Player").GetComponent<PlayerConversant>();
            AIText.SetText(_playerConversant.GetText());
        }
    }
}