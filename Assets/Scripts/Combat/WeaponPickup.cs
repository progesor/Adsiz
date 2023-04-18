using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private float respawnTime = 5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
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

    }
}