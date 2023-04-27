using System;
using ProgesorCreating.Quests;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] private QuestItemUI questPrefab;

        private QuestList _questList;

        private void Start()
        {
            _questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            _questList.OnUpdate += Redraw;
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform item in this.transform)
            {
                Destroy(item.gameObject);
            }

            foreach (QuestStatus status in _questList.GetStatuses())
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
                uiInstance.Setup(status);
            }
        }
    }
}