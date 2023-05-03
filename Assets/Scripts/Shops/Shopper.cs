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
            if (activeShop!=null)
            {
                activeShop.SetShopper(null);
            }
            activeShop = shop;
            if (activeShop!=null)
            {
                activeShop.SetShopper(this);
            }
            ActiveShopChange?.Invoke();
        }

        public Shop GetActiveShop()
        {
            return activeShop;
        }
    }
}