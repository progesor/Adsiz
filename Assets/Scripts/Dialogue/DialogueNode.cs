using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    [Serializable]
    public class DialogueNode
    {
        public string uniqueID;
        public string text;
        public string[] children;
        public Rect rect = new Rect(0, 0, 200, 100);
    }
}