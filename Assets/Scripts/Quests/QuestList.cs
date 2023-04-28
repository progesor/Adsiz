using System;
using System.Collections.Generic;
using ProgesorCreating.Saving;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Quests
{
    public class QuestList : MonoBehaviour,ISaveable
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

        public void CompleteObjective(Quest quest, string objective)
        {
            QuestStatus status = GetQuestStatus(quest);
            status.CompleteObjective(objective);
            if (OnUpdate != null) OnUpdate();
        }

        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return _statuses;
        }

        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in _statuses)
            {
                if (status.GetQuest()==quest)
                {
                    return status;
                }
            }

            return null;
        }

        public object CaptureState()
        {
            List<object> state = new List<object>();
            foreach (QuestStatus status in _statuses)
            {
                state.Add(status.CaptureState());
            }

            return state;
        }

        public void RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList==null)return;
            
            _statuses.Clear();

            foreach (object objectState in stateList)
            {
                _statuses.Add(new QuestStatus(objectState));

            }
        }
    }
}