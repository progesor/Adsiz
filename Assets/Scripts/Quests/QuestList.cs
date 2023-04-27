using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Quests
{
    public class QuestList : MonoBehaviour
    {
        [SerializeField] private QuestStatus[] statuses;

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }
    }
}