using ProgesorCreating.Core.UI.Tooltips;
using ProgesorCreating.Quests;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI.Quests
{
    public class QuestTooltipSpawner : TooltipSpawner
    {
        public override void UpdateTooltip(GameObject tooltip)
        {
            Quest quest = GetComponent<QuestItemUI>().GetQuest();
            tooltip.GetComponent<QuestTooltipUI>().Setup(quest);
        }

        public override bool CanCreateTooltip()
        {
            return true;
        }
    }
}