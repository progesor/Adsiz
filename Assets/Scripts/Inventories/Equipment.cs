﻿using System.Collections.Generic;
using ProgesorCreating.Saving;
using UnityEngine;
using System;
using ProgesorCreating.Utils;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Inventories
{
    /// <summary>
    /// Provides a store for the items equipped to a player. Items are stored by
    /// their equip locations.
    /// 
    /// This component should be placed on the GameObject tagged "Player".
    /// </summary>
    public class Equipment : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        // STATE
        Dictionary<EquipLocation, EquipableItem> _equippedItems = new Dictionary<EquipLocation, EquipableItem>();

        // PUBLIC

        /// <summary>
        /// Broadcasts when the items in the slots are added/removed.
        /// </summary>
        public event Action EquipmentUpdated;

        /// <summary>
        /// Return the item in the given equip location.
        /// </summary>
        public EquipableItem GetItemInSlot(EquipLocation equipLocation)
        {
            if (!_equippedItems.ContainsKey(equipLocation))
            {
                return null;
            }

            return _equippedItems[equipLocation];
        }

        /// <summary>
        /// Add an item to the given equip location. Do not attempt to equip to
        /// an incompatible slot.
        /// </summary>
        public void AddItem(EquipLocation slot, EquipableItem item)
        {
            Debug.Assert(item.CanEquip(slot,this));

            _equippedItems[slot] = item;

            if (EquipmentUpdated != null)
            {
                EquipmentUpdated();
            }
        }

        /// <summary>
        /// Remove the item for the given slot.
        /// </summary>
        public void RemoveItem(EquipLocation slot)
        {
            _equippedItems.Remove(slot);
            if (EquipmentUpdated != null)
            {
                EquipmentUpdated();
            }
        }

        /// <summary>
        /// Enumerate through all the slots that currently contain items.
        /// </summary>
        public IEnumerable<EquipLocation> GetAllPopulatedSlots()
        {
            return _equippedItems.Keys;
        }

        // PRIVATE

        object ISaveable.CaptureState()
        {
            var equippedItemsForSerialization = new Dictionary<EquipLocation, string>();
            foreach (var pair in _equippedItems)
            {
                equippedItemsForSerialization[pair.Key] = pair.Value.GetItemID();
            }
            return equippedItemsForSerialization;
        }

        void ISaveable.RestoreState(object state)
        {
            _equippedItems = new Dictionary<EquipLocation, EquipableItem>();

            var equippedItemsForSerialization = (Dictionary<EquipLocation, string>)state;

            foreach (var pair in equippedItemsForSerialization)
            {
                var item = (EquipableItem)InventoryItem.GetFromID(pair.Value);
                if (item != null)
                {
                    _equippedItems[pair.Key] = item;
                }
            }
            
            EquipmentUpdated?.Invoke();
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            if (predicate=="HasItemEquiped")
            {
                foreach (EquipableItem item in _equippedItems.Values)
                {
                    if (item.GetItemID()==parameters[0])
                    {
                        return true;
                    }
                }

                return false;
            }

            return null;
        }
    }
}