using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    [Serializable]
    public class DialogueNode
    {
        public string uniqueID;
        public string text;
        public List<string> children = new List<string>();
        public Rect rect = new Rect(0, 0, 200, 100);
    }
}