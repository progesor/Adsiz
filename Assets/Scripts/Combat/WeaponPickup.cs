﻿using System;
using System.Collections;
using ProgesorCreating.RPG.Control;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    public class WeaponPickup : MonoBehaviour,IRaycastable
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private float respawnTime = 5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Pickup(other.GetComponent<Fighter>());
            }
        }

        private void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
            
        }

        private void ShowPickup(bool shouldShow)
        {

            GetComponent<Rigidbody>().useGravity = shouldShow;
            GetComponent<Collider>().enabled = shouldShow;
            

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButton(0))
            {
                Pickup(callingController.GetComponent<Fighter>());
            }

            return true;
        }
    }
}