using ProgesorCreating.Shops;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProgesorCreating.UI.Shops
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField] private Image iconField;
        [SerializeField] private TextMeshProUGUI nameField;
        [SerializeField] private TextMeshProUGUI availabiltyField;
        [SerializeField] private TextMeshProUGUI priceField;

        private Shop _currentShop;
        private ShopItem _item;
        public void Setup(Shop currentShop, ShopItem item)
        {
            _currentShop = currentShop;
            _item = item;
            iconField.sprite = item.GetIcon();
            nameField.text = item.GetName();
            availabiltyField.text = $"{item.GetAvailability()}";
            priceField.text = $"$ {item.GetPrice():N2}";
        }

        public void Add()
        {
            _currentShop.AddToTransaction(_item.GetInventoryItem(),1);
        }

        public void Remove()
        {
            _currentShop.AddToTransaction(_item.GetInventoryItem(),-1);
        }
    }
}