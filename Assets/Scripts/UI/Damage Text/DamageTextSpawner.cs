﻿using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.UI.Damage_Text
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private DamageText damageTextPrefab = null;
        private void Start()
        {
            Spawn(10);
        }

        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }
}