using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/New Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private List<DialogueNode> nodes = new List<DialogueNode>();

        private Dictionary<string, DialogueNode> _nodeLookup = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR
        private void Awake()
        {
            if (nodes.Count == 0)
            {
                nodes.Add(new DialogueNode());
            }
            OnValidate();
        }
#endif

        private void OnValidate()
        {
            _nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                _nodeLookup[node.uniqueID] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childId in parentNode.children)
            {
                if (_nodeLookup.ContainsKey(childId))
                {
                    yield return _nodeLookup[childId];
                }
            }
        }
    }
}
