using System;
using System.Collections.Generic;
using ProgesorCreating.Control;
using ProgesorCreating.Inventories;
using ProgesorCreating.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProgesorCreating.Shops
{
    public class Shop : MonoBehaviour, IRaycastable
    {

        [SerializeField] private string shopName;
        [FormerlySerializedAs("sellingDiscountPercentage")]
        [Range(0,100)]
        [SerializeField] private float sellingPercentage = 50f;
        [SerializeField] private StockItemConfig[] stockConfig;

        private Shopper _currentShopper;
        private Dictionary<InventoryItem, int> _transaction = new Dictionary<InventoryItem, int>();
        private Dictionary<InventoryItem, int> _stock = new Dictionary<InventoryItem, int>();
        private bool _isBuyingMode = true;
        private ItemCategory _filter = ItemCategory.None;

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
            foreach (ShopItem shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                if (_filter == ItemCategory.None || item.GetCategory()==_filter)
                {
                    yield return shopItem;
                }
            }
        }

        public IEnumerable<ShopItem> GetAllItems()
        {
            Dictionary<InventoryItem, float> prices = GetPrices();
            Dictionary<InventoryItem, int> avilabilities = GetAvailabilities();
            foreach (InventoryItem item in avilabilities.Keys)
            {
                if (avilabilities[item]<=0)continue;

                float price = prices[item];
                int quantityInTransaction = 0;
                _transaction.TryGetValue(item, out quantityInTransaction);
                int availability = avilabilities[item];
                yield return new ShopItem(item, availability, price, quantityInTransaction); 
            }
        }

        public void SelectFilter(ItemCategory category)
        {
            _filter = category;
            
            OnChange?.Invoke();
        }

        public ItemCategory GetFilter()
        {
            return _filter;
        }

        public void SelectMode(bool isBuying)
        {
            _isBuyingMode = isBuying;
            OnChange?.Invoke();
        }

        public bool IsBuyingMode()
        {
            return _isBuyingMode;
        }

        public bool CanTransact()
        {
            if (isTransactionEmpty()) return false;
            if (!HasSufficientFunds()) return false;
            if (!HasInventorySpace()) return false;
                
            return true;
        }

        public bool HasSufficientFunds()
        {
            if (!_isBuyingMode) return true;
            Purse purse = _currentShopper.GetComponent<Purse>();
            if (purse == null) return false;

            return purse.GetBalance() >= TransactionTotal();
        }

        public bool isTransactionEmpty()
        {
            return _transaction.Count == 0;
        }

        public bool HasInventorySpace()
        {
            if (!_isBuyingMode) return true;
            Inventory shopperInventory = _currentShopper.GetComponent<Inventory>();
            if (shopperInventory == null) return false;
            
            List<InventoryItem> flatItems = new List<InventoryItem>();
            foreach (ShopItem shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                for (int i = 0; i < quantity; i++)
                {
                    flatItems.Add(item);
                }
            }

            return shopperInventory.HasSpaceFor(flatItems);
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
                    if (_isBuyingMode)
                    {
                        BuyItem(shopperInventory, shopperPurse, item, price);
                    }
                    else
                    {
                        SellItem(shopperInventory, shopperPurse, item, price);
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

            int availability = GetAvailability(item);

            if (_transaction[item]+quantity>availability)
            {
                _transaction[item] = availability;
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
        
        private int GetAvailability(InventoryItem item)
        {
            if (_isBuyingMode)
            {
                return _stock[item];
            }

            return CountItemsInInventory(item);

        }

        private int CountItemsInInventory(InventoryItem item)
        {
            Inventory inventory = _currentShopper.GetComponent<Inventory>();
            if (inventory == null) return 0;

            int total = 0;
            for (int i = 0; i < inventory.GetSize(); i++)
            {
                if (inventory.GetItemInSlot(i)==item)
                {
                    total += inventory.GetNumberInSlot(i);
                }
            }

            return total;
        }

        private float GetPrice(StockItemConfig config)
        {
            if (_isBuyingMode)
            {
                return config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
            }

            return config.item.GetPrice() * (sellingPercentage / 100);

        }

        private Dictionary<InventoryItem, int> GetAvailabilities()
        {
            Dictionary<InventoryItem, int> availabilities = new Dictionary<InventoryItem, int>();

            foreach (StockItemConfig config in GetAvailableConfigs())
            {
                if (!availabilities.ContainsKey(config.item))
                {
                    availabilities[config.item] = 0;
                }
                availabilities[config.item] += config.initialStock;
            }
            return availabilities;
        }

        private Dictionary<InventoryItem, float> GetPrices()
        {
            Dictionary<InventoryItem, float> prices = new Dictionary<InventoryItem, float>();

            foreach (StockItemConfig config in GetAvailableConfigs())
            {
                if (!prices.ContainsKey(config.item))
                {
                    prices[config.item] = config.item.GetPrice();
                }

                prices[config.item] *= (1 - config.buyingDiscountPercentage / 100);
            }
            
            return prices;
        }

        public IEnumerable<StockItemConfig> GetAvailableConfigs()
        {
            int shopperLevel = GetShopperLevel();
            foreach (StockItemConfig config in stockConfig)
            {
                if (config.levelToUnlock>shopperLevel)continue;
                yield return config;
            }
        }

        private void SellItem(Inventory shopperInventory, Purse shopperPurse, InventoryItem item, float price)
        {
            int slot = FindFirstItemSlot(shopperInventory, item);
            if (slot==-1)return;
            
            AddToTransaction(item, -1);
            shopperInventory.RemoveFromSlot(slot,1);
            _stock[item]++;
            shopperPurse.UpdateBalance(price);
        }

        private void BuyItem(Inventory shopperInventory, Purse shopperPurse,  InventoryItem item, float price)
        {
            if (shopperPurse.GetBalance() < price) return;

            bool success = shopperInventory.AddToFirstEmptySlot(item, 1);
            if (success)
            {
                AddToTransaction(item, -1);
                _stock[item]--;
                shopperPurse.UpdateBalance(-price);
            }
        }

        private int FindFirstItemSlot(Inventory shopperInventory, InventoryItem item)
        {
            for (int i = 0; i < shopperInventory.GetSize(); i++)
            {
                if (shopperInventory.GetItemInSlot(i)==item)
                {
                    return i;
                }
            }

            return -1;
        }

        private int GetShopperLevel()
        {
            BaseStats stats = _currentShopper.GetComponent<BaseStats>();
            if (stats == null) return 0;

            return stats.GetLevel();
        }
    }
}