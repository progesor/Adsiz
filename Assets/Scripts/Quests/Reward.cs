using System;
using ProgesorCreating.Inventories;
using UnityEngine;

namespace ProgesorCreating.Quests
{
    [Serializable]
    public class Reward
    {
        [Min(1)]
        public int Number;
        public InventoryItem Item;
    }
}