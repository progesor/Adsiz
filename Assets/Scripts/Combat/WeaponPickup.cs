﻿using System.Collections;
using ProgesorCreating.Attributes;
using ProgesorCreating.Control;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Combat
{
    public class WeaponPickup : MonoBehaviour,IRaycastable
    {
        [FormerlySerializedAs("weapon")] [SerializeField] private WeaponConfig weaponConfig;
        [SerializeField] private float healthToRestore;
        [SerializeField] private float respawnTime = 5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Pickup(other.gameObject);
            }
        }

        private void Pickup(GameObject subject)
        {
            if (weaponConfig!=null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weaponConfig);
            }

            if (healthToRestore>0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }
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

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButton(0))
            {
                Pickup(callingController.gameObject);
            }

            return true;
        }
    }
}