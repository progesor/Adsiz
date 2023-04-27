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

        private QuestStatus _status;
        
        public void Setup(QuestStatus status)
        {
            _status = status;
            title.text = status.GetQuest().GetTitle();
            progress.text = string.Format("{0}/{1}",status.GetCompletedCount(), status.GetQuest().GetObjectiveCount());
        }

        public QuestStatus GetQuestStatus()
        {
            return _status;
        }
    }
}