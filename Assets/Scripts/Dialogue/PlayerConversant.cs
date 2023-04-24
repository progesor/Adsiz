using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] private Dialogue currentDialogue;

        public string GetText()
        {
            if (currentDialogue==null)
            {
                return string.Empty;
            }

            return currentDialogue.GetRootNode().GetText();
        }
    }
}