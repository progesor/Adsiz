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
        [SerializeField] private Transform listRoot;
        [SerializeField] private RowUI rowPrefab;
        
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
            if (_currentShop != null)
            {
                _currentShop.OnChange -= RefreshUI;
            }
            
            _currentShop = _shopper.GetActiveShop();
            gameObject.SetActive(_currentShop != null);

            if (_currentShop==null)return;
            shopName.text = _currentShop.GetShopName();

            _currentShop.OnChange += RefreshUI;

            RefreshUI();
        }

        private void RefreshUI()
        {
            foreach (Transform child in listRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (ShopItem item in _currentShop.GetFilteredItems())
            {
                RowUI row = Instantiate<RowUI>(rowPrefab, listRoot);
                row.Setup(_currentShop, item);
            }
        }

        public void Close()
        {
            _shopper.SetActiveShop(null);
        }
    }
}