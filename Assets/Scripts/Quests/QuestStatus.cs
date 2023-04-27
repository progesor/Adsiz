using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Quests
{
    [Serializable]
    public class QuestStatus 
    {
        [SerializeField] private Quest quest;
        [SerializeField] private List<string> completedObjectives;

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompletedCount()
        {
            return completedObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective)
        {
            return completedObjectives.Contains(objective);
        }
    }
}