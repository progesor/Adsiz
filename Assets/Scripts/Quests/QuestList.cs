using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Quests
{
    public class QuestList : MonoBehaviour
    {
        private List<QuestStatus> _statuses = new List<QuestStatus>();

        public event Action OnUpdate;

        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest))return;
            QuestStatus newStatus = new QuestStatus(quest);
            _statuses.Add(newStatus);
            if (OnUpdate != null) OnUpdate();
        }

        public bool HasQuest(Quest quest)
        {
            foreach (QuestStatus status in _statuses)
            {
                if (status.GetQuest()==quest)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return _statuses;
        }
    }
}