using System;
using System.Collections.Generic;
using System.Linq;
using ProgesorCreating.Core;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] private string playerName;
        
        private Dialogue _currentDialogue;
        private DialogueNode _currentNode;
        private bool _isChoosing;
        private AIConversant _currentConversant;

        public event Action OnConversationUpdated;

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            _currentConversant = newConversant;
            _currentDialogue = newDialogue;
            _currentNode = _currentDialogue.GetRootNode();
            TriggerEnterAction();
            if (OnConversationUpdated != null) OnConversationUpdated();
        }

        public void Quit()
        {
            _currentDialogue = null;
            TriggerExitAction();
            _currentNode = null;
            _isChoosing = false;
            _currentConversant = null;
            if (OnConversationUpdated != null) OnConversationUpdated();
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

        public string GetCurrentConversantName()
        {
            if (_isChoosing)
            {
                return playerName;
            }
            else
            {
                return _currentConversant.GetName();
            }
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(_currentDialogue.GetPlayerChildren(_currentNode));
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
            int numPlayerResponses = FilterOnCondition(_currentDialogue.GetPlayerChildren(_currentNode)).Count();

            if (numPlayerResponses>0)
            {
                _isChoosing = true;
                TriggerExitAction();
                if (OnConversationUpdated != null) OnConversationUpdated();
                return;
            }

            DialogueNode[] children = FilterOnCondition(_currentDialogue.GetAIChildren(_currentNode)).ToArray();
            int randomIndex = Random.Range(0, children.Count());
            TriggerExitAction();
            _currentNode = children[randomIndex];
            TriggerEnterAction();
            if (OnConversationUpdated != null) OnConversationUpdated();
        }

        public bool HasNext()
        {
            return FilterOnCondition(_currentDialogue.GetAllChildren(_currentNode)).Any();
        }

        private void TriggerEnterAction()
        {
            if (_currentNode!=null)
            {
                TriggerAction(_currentNode.GetOnEnterAction());
            }
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (DialogueNode node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }

        private void TriggerExitAction()
        {
            if (_currentNode!=null)
            {
                TriggerAction(_currentNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action==String.Empty)return;

            foreach (DialogueTrigger trigger in _currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
            
        }
    }
}