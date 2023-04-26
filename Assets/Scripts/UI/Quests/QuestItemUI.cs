using ProgesorCreating.Quests;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI.Quests
{
    public class QuestItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI progress;

        private Quest _quest;
        
        public void Setup(Quest quest)
        {
            _quest = quest;
            title.text = quest.GetTitle();
            progress.text = string.Format("0/{0}", quest.GetObjectiveCount());
        }

        public Quest GetQuest()
        {
            return _quest;
        }
    }
}