using System;
using UnityEngine;

namespace ProgesorCreating.Shops
{
    public class Shopper : MonoBehaviour
    {
        private Shop activeShop = null;

        public event Action ActiveShopChange;
        public void SetActiveShop(Shop shop)
        {
            activeShop = shop;
            ActiveShopChange?.Invoke();
        }

        public Shop GetActiveShop()
        {
            return activeShop;
        }
    }
}