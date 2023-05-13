using System;
using ProgesorCreating.Control;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        private PlayerController _playerController;
        private void Start()
        {
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
            _playerController.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
            _playerController.gameObject.SetActive(true);
        }
    }
}