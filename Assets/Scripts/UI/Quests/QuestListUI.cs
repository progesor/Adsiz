using System;
using ProgesorCreating.Quests;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] private QuestItemUI questPrefab;

        private void Start()
        {
            foreach (Transform item in this.transform)
            {
                Destroy(item.gameObject);
            }

            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            
            foreach (QuestStatus status in questList.GetStatuses())
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
                uiInstance.Setup(status);
            }
        }
    }
}