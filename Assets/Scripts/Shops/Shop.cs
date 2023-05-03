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
        [SerializeField] private StockItemConfig[] stockConfig;

        private Shopper _currentShopper;
        private Dictionary<InventoryItem, int> _transaction = new Dictionary<InventoryItem, int>();
        private Dictionary<InventoryItem, int> _stock = new Dictionary<InventoryItem, int>();

        public event Action OnChange;

        private void Awake()
        {
            foreach (StockItemConfig config in stockConfig)
            {
                _stock[config.item] = config.initialStock;
            }
        }

        public void SetShopper(Shopper shopper)
        {
            _currentShopper = shopper;
        }
        
        public IEnumerable<ShopItem> GetFilteredItems()
        {
            return GetAllItems();
        }

        public IEnumerable<ShopItem> GetAllItems()
        {
            foreach (StockItemConfig config in stockConfig)
            {
                float price = config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
                int quantityInTransaction = 0;
                _transaction.TryGetValue(config.item, out quantityInTransaction);
                int currentStock = _stock[config.item];
                yield return new ShopItem(config.item, currentStock, price, quantityInTransaction); 
            }
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
            Inventory shopperInventory = _currentShopper.GetComponent<Inventory>();
            Purse shopperPurse = _currentShopper.GetComponent<Purse>();
            if (shopperInventory == null || shopperPurse == null) return;

            foreach (ShopItem shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                float price = shopItem.GetPrice();
                for (int i = 0; i < quantity; i++)
                {
                    if (shopperPurse.GetBalance()<price) break;
                    
                    bool success = shopperInventory.AddToFirstEmptySlot(item, 1);
                    if (success)
                    {
                        AddToTransaction(item,-1);
                        _stock[item]--;
                        shopperPurse.UpdateBalance(-price);
                    }
                }
            }
            
            OnChange?.Invoke();
            
        }

        public float TransactionTotal()
        {
            float total = 0;
            foreach (ShopItem item in GetAllItems())
            {
                total += item.GetPrice() * item.GetQuantityInTransaction();
            }

            return total;
        }

        public void AddToTransaction(InventoryItem item, int quantity)
        {
            if (!_transaction.ContainsKey(item))
            {
                _transaction[item] = 0;
            }

            if (_transaction[item]+quantity>_stock[item])
            {
                _transaction[item] = _stock[item];
            }
            else
            {
                _transaction[item] += quantity;
            }
            

            if (_transaction[item] <= 0)
            {
                _transaction.Remove(item);
            }

            OnChange?.Invoke();
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