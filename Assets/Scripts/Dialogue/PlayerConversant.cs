using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] private Dialogue currentDialogue;

        private DialogueNode currentNode;
        private bool isChoosing;

        private void Awake()
        {
            currentNode = currentDialogue.GetRootNode();
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetText()
        {
            if (currentNode==null)
            {
                return string.Empty;
            }

            return currentNode.GetText();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            isChoosing = false;
            Next();
        }

        public void Next()
        {
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();

            if (numPlayerResponses>0)
            {
                isChoosing = true;
                return;
            }
            
            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int randomIndex = Random.Range(0, children.Count());
            currentNode = children[randomIndex];
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }
    }
}