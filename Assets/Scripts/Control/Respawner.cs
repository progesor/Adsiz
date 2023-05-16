using System;
using System.Collections;
using Cinemachine;
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
        [SerializeField] private float enemyHealthRegenPercentage = 20;
        
        private Vector3 _startLocation;
        private void Awake()
        {
            GetComponent<Health>().onDie.AddListener(Respawn);
        }

        private void Start()
        {
            if (GetComponent<Health>().IsDead())
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
            yield return new WaitForSeconds(respawnDelay);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime);
            RespawnPlayer();
            ResetEnemies();
            savingWrapper.Save();
            yield return fader.FadeIn(fadeTime);
        }

        private void ResetEnemies()
        {
            foreach (AIController enemyController in FindObjectsOfType<AIController>())
            {
                Health health = enemyController.GetComponent<Health>();
                if (health && !health.IsDead())
                {
                    enemyController.Reset();
                    health.Heal(health.GetHealthPoint() * enemyHealthRegenPercentage / 100);
                }
            }
        }

        private void RespawnPlayer()
        {
            Vector3 positionDelta = resPawnLocation.position - transform.position;
            GetComponent<NavMeshAgent>().Warp(resPawnLocation.position);
            Health health = GetComponent<Health>();
            health.Heal(health.GetHealthPoint() * healthRegenPercentage / 100);
            ICinemachineCamera activeVirtualCamera = FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;
            if (activeVirtualCamera.Follow==transform)
            {
                activeVirtualCamera.OnTargetObjectWarped(transform, positionDelta);
            }
        }
    }
}