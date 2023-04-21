using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.UI.Inventories
{
    public class InventorySlotUI : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon = null;

        // PUBLIC

        public int MaxAcceptable(Sprite item)
        {
            if (GetItem() == null)
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void AddItems(Sprite item, int number)
        {
            icon.SetItem(item);
        }

        public Sprite GetItem()
        {
            return icon.GetItem();
        }

        public int GetNumber()
        {
            return 1;
        }

        public void RemoveItems(int number)
        {
            icon.SetItem(null);
        }
    }
}