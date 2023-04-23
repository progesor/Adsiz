using System;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    [Serializable]
    public class DialogueNode
    {
        public string uniqueID;
        public string text;
        public string[] children;
    }
}