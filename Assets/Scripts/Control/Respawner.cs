using System;
using System.Collections;
using ProgesorCreating.Attributes;
using ProgesorCreating.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Control
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] private Transform resPawnLocation;
        [SerializeField] private float respawnDelay = 3.0f;
        [SerializeField] private float fadeTime = 0.2f;
        [SerializeField] private float healthRegenPercentage = 20;
        
        private Vector3 _startLocation;
        private void Awake()
        {
            GetComponent<Health>().onDie.AddListener(Respawn);
        }

        private void Respawn()
        {
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(respawnDelay);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime);
            GetComponent<NavMeshAgent>().Warp(resPawnLocation.position);
            Health health = GetComponent<Health>();
            health.Heal(health.GetHealthPoint() * healthRegenPercentage / 100);
            yield return fader.FadeIn(fadeTime);
        }
    }
}