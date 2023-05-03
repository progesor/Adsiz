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
        public void Setup(ShopItem item)
        {
            iconField.sprite = item.GetIcon();
            nameField.text = item.GetName();
            availabiltyField.text = $"{item.GetAvailability()}";
            priceField.text = $"$ {item.GetPrice():N2}";
        }
    }
}