﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        private Dialogue _currentDialogue;
        private DialogueNode _currentNode;
        private bool _isChoosing;

        public event Action OnConversationUpdated;

        public void StartDialogue(Dialogue newDialogue)
        {
            _currentDialogue = newDialogue;
            _currentNode = _currentDialogue.GetRootNode();
            TriggerEnterAction();
            OnConversationUpdated();
        }

        public void Quit()
        {
            _currentDialogue = null;
            TriggerExitAction();
            _currentNode = null;
            _isChoosing = false;
            OnConversationUpdated();
        }

        public bool IsActive()
        {
            return _currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return _isChoosing;
        }

        public string GetText()
        {
            if (_currentNode==null)
            {
                return string.Empty;
            }

            return _currentNode.GetText();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return _currentDialogue.GetPlayerChildren(_currentNode);
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            _currentNode = chosenNode;
            TriggerEnterAction();
            _isChoosing = false;
            Next();
        }

        public void Next()
        {
            int numPlayerResponses = _currentDialogue.GetPlayerChildren(_currentNode).Count();

            if (numPlayerResponses>0)
            {
                _isChoosing = true;
                TriggerExitAction();
                OnConversationUpdated();
                return;
            }
            
            DialogueNode[] children = _currentDialogue.GetAIChildren(_currentNode).ToArray();
            int randomIndex = Random.Range(0, children.Count());
            TriggerExitAction();
            _currentNode = children[randomIndex];
            TriggerEnterAction();
            OnConversationUpdated();
        }

        public bool HasNext()
        {
            return _currentDialogue.GetAllChildren(_currentNode).Count() > 0;
        }

        private void TriggerEnterAction()
        {
            if (_currentNode!=null && _currentNode.GetOnEnterAction()!=string.Empty)
            {
                Debug.Log(_currentNode.GetOnEnterAction());
            }
        }
        
        private void TriggerExitAction()
        {
            if (_currentNode!=null && _currentNode.GetOnExitAction()!=string.Empty)
            {
                Debug.Log(_currentNode.GetOnExitAction());
            }
        }
    }
}