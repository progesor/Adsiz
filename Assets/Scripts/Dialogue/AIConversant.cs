using ProgesorCreating.Control;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    public class AIConversant : MonoBehaviour,IRaycastable
    {
        [SerializeField] private Dialogue dialogue;
        public CursorType getCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null) return false;
            
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(dialogue);
            }
            return true;
        }
    }
}