using System;
using ProgesorCreating.Saving;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Inventories
{
    public class Purse : MonoBehaviour,ISaveable, IItemStore
    {
        [SerializeField] private float startingBalance = 400f;

        private float _balance;

        public event Action OnChange;

        private void Awake()
        {
            _balance = startingBalance;
        }

        public float GetBalance()
        {
            return _balance;
        }

        public void UpdateBalance(float amount)
        {
            _balance += amount;
            OnChange?.Invoke();
        }

        public object CaptureState()
        {
            return _balance;
        }

        public void RestoreState(object state)
        {
            _balance = (float)state;
        }

        public int AddItems(InventoryItem item, int number)
        {
            if (item is CurrencyItem)
            {
                UpdateBalance(item.GetPrice()*number);
                return number;
            }

            return 0;
        }
    }
}