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
        [SerializeField] private TextMeshProUGUI totalField;
        [SerializeField] private Button confirmButton;
        
        private Shopper _shopper;
        private Shop _currentShop;

        private Color originalTotalTextColor;
        private void Start()
        {
            originalTotalTextColor = totalField.color;
            _shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
            if (_shopper==null)return;

            _shopper.ActiveShopChange += ShopChanged;
            confirmButton.onClick.AddListener(ConfirmTransaction);
            
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

            totalField.text = $"Total: $ {_currentShop.TransactionTotal():N2}";
            totalField.color = _currentShop.HasSufficientFunds() ? originalTotalTextColor : Color.red;
            confirmButton.interactable = _currentShop.CanTransact();
        }

        public void Close()
        {
            _shopper.SetActiveShop(null);
        }

        public void ConfirmTransaction()
        {
            _currentShop.ConfirmTransaction();
        }
    }
}