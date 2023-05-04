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
        [SerializeField] private Button switchButton;
        
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
            switchButton.onClick.AddListener(SwitchMode);
            
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

            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.SetShop(_currentShop);
            }

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
            TextMeshProUGUI switchText = switchButton.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI confirmText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();

            if (_currentShop.IsBuyingMode())
            {
                switchText.text = "Switch To Selling";
                confirmText.text = "Buy";
            }
            else
            {
                switchText.text = "Switch To Buying";
                confirmText.text = "Sell";
            }
        }

        public void Close()
        {
            _shopper.SetActiveShop(null);
        }

        public void ConfirmTransaction()
        {
            _currentShop.ConfirmTransaction();
        }

        public void SwitchMode()
        {
            _currentShop.SelectMode(!_currentShop.IsBuyingMode());
        }
    }
}