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
        
        public void Setup(Quest quest)
        {
            title.text = quest.GetTitle();
            progress.text = string.Format("0/{0}", quest.GetObjectiveCount());
        }
    }
}