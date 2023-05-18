using System;
using System.Collections.Generic;
using ProgesorCreating.Inventories;
using ProgesorCreating.Saving;
using ProgesorCreating.Utils;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Quests
{
    public class QuestList : MonoBehaviour,ISaveable,IPredicateEvaluator
    {
        private List<QuestStatus> _statuses = new List<QuestStatus>();

        public event Action OnUpdate;

        private void Update()
        {
            CompleteObjectivesByPredicates();
        }

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
            if (status.IsComplete())
            {
                GiveReward(quest);
            }
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

        private void GiveReward(Quest quest)
        {
            foreach (Reward reward in quest.GetRewards())
            {
                bool success = GetComponent<Inventory>().AddToFirstEmptySlot(reward.Item, reward.Number);
                if (!success)
                {
                    GetComponent<ItemDropper>().DropItem(reward.Item,reward.Number);
                }
            }
        }

        private void CompleteObjectivesByPredicates()
        {
            foreach (QuestStatus status in _statuses)
            {
                if (status.IsComplete())continue;

                Quest quest = status.GetQuest();
                foreach (Objective objective in quest.GetObjectives())
                {
                    if (status.IsObjectiveComplete(objective.reference))continue;
                    if (!objective.usesCondition)continue;
                    if (objective.CompletiCondition.Check(GetComponents<IPredicateEvaluator>()))
                    {
                        CompleteObjective(quest, objective.reference);
                    }
                }
            }
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

        public bool? Evaluate(EPredicate predicate, string[] parameters)
        {
            switch (predicate)
            {
                case EPredicate.HasQuest: 
                    return HasQuest(Quest.GetByName(parameters[0]));
                case EPredicate.CompletedQuest:
                    QuestStatus status = GetQuestStatus(Quest.GetByName(parameters[0]));
                    if (status == null) return false;
                    return status.IsComplete(); 
                    return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
                case EPredicate.CompletedObjective:
                    QuestStatus teststatus = GetQuestStatus(Quest.GetByName(parameters[0]));
                    if (teststatus==null) return false;
                    return teststatus.IsObjectiveComplete(parameters[1]);
            }

            return null;
        }
    }
}