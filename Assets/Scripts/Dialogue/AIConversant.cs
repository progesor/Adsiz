﻿using ProgesorCreating.Control;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    public class AIConversant : MonoBehaviour,IRaycastable
    {
        [SerializeField] private Dialogue dialogue;
        [SerializeField] private string conversantName;
        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null) return false;
            
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }
            return true;
        }

        public string GetName()
        {
            return conversantName;
        }
    }
}