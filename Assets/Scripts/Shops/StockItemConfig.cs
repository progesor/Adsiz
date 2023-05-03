using System;
using ProgesorCreating.Inventories;
using UnityEngine;

namespace ProgesorCreating.Shops
{
    [Serializable]
    public class StockItemConfig
    {
        public InventoryItem item;
        public int initialStock;
        [Range(0,100)]
        public float buyingDiscountPercentage;
    }
}