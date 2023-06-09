﻿using System;
using ProgesorCreating.Inventories;
using ProgesorCreating.Shops;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI.Shops
{
    public class FilterButtonUI : MonoBehaviour
    {
        [SerializeField] private ItemCategory category = ItemCategory.None;
        
        private Button _button;
        private Shop _currentShop;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SelectFilter);
        }

        public void SetShop(Shop currentShop)
        {
            _currentShop = currentShop;
        }

        public void RefreshUI()
        {
            _button.interactable = _currentShop.GetFilter() != category;
        }

        private void SelectFilter()
        {
            _currentShop.SelectFilter(category);
        }
    }
}