using System;
using ProgesorCreating.Quests;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] private Quest[] tempQuests;
        [SerializeField] private QuestItemUI questPrefab;

        private void Start()
        {
            foreach (Transform item in this.transform)
            {
                Destroy(item.gameObject);
            }
            
            foreach (Quest quest in tempQuests)
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
                uiInstance.Setup(quest);
            }
        }
    }
}