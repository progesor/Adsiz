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
            return null;
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