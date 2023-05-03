using System;
using ProgesorCreating.Shops;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProgesorCreating.UI.Shops
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI shopName;
        
        private Shopper _shopper;
        private Shop _currentShop;
        private void Start()
        {
            _shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
            if (_shopper==null)return;

            _shopper.ActiveShopChange += ShopChanged;
            
            ShopChanged();
        }

        private void ShopChanged()
        {
            _currentShop = _shopper.GetActiveShop();
            gameObject.SetActive(_currentShop != null);

            if (_currentShop==null)return;
            shopName.text = _currentShop.GetShopName();
        }

        public void Close()
        {
            _shopper.SetActiveShop(null);
        }
    }
}