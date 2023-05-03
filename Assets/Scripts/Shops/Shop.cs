using System;
using System.Collections.Generic;
using ProgesorCreating.Control;
using ProgesorCreating.Inventories;
using UnityEngine;

namespace ProgesorCreating.Shops
{
    public class Shop : MonoBehaviour, IRaycastable
    {

        [SerializeField] private string shopName;

        public event Action OnChange;
        
        public IEnumerable<ShopItem> GetFilteredItems()
        {
            yield return new ShopItem(InventoryItem.GetFromID("1884d6b3-d865-4f45-84d2-250e8f9871ea"), 13, 24.50f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("97935419-207b-424c-ba6c-86409fc3d10d"), 5, 14.00f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("3b056e0f-42dd-40aa-bd7c-3209c3f78dac"), 2, 824.50f, 0);
        }

        public void SelectFilter(ItemCategory category)
        {
            
        }

        public ItemCategory GetFilter()
        {
            return ItemCategory.None;
        }

        public void SelectMode(bool isBuying)
        {
            
        }

        public bool IsBuyingMode()
        {
            return true;
        }

        public bool CanTransact()
        {
            return true;
        }

        public void ConfirmTransaction()
        {
            
        }

        public float TransactionTotal()
        {
            return 0;
        }

        public void AddToTransaction(InventoryItem item, int quantity)
        {
            
        }

        public CursorType getCursorType()
        {
            return CursorType.Shop;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Shopper>().SetActiveShop(this);
            }

            return true;
        }

        public string GetShopName()
        {
            return shopName;
        }
    }
}