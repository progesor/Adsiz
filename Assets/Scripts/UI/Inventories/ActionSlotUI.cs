using ProgesorCreating.Abilities;
using ProgesorCreating.Core.UI.Dragging;
using ProgesorCreating.Inventories;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI.Inventories
{
    /// <summary>
    /// The UI slot for the player action bar.
    /// </summary>
    public class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon;
        [SerializeField] int index;
        [SerializeField] private Image cooldownOverlay;

        // CACHE
        ActionStore _store;
        private CooldownStore _cooldownStore;

        // LIFECYCLE METHODS
        private void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _store = player.GetComponent<ActionStore>();
            _cooldownStore = player.GetComponent<CooldownStore>();
            _store.StoreUpdated += UpdateIcon;
        }

        private void Update()
        {
            cooldownOverlay.fillAmount = _cooldownStore.GetFractionRemaining(GetItem());
        }

        // PUBLIC

        public void AddItems(InventoryItem item, int number)
        {
            _store.AddAction(item, index, number);
        }

        public InventoryItem GetItem()
        {
            return _store.GetAction(index);
        }

        public int GetNumber()
        {
            return _store.GetNumber(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return _store.MaxAcceptable(item, index);
        }

        public void RemoveItems(int number)
        {
            _store.RemoveItems(index, number);
        }

        // PRIVATE

        void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetNumber());
        }
    }
}