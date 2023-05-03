using System;
using ProgesorCreating.Inventories;
using TMPro;
using UnityEngine;

namespace ProgesorCreating.UI.Inventories
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI balanceField;

        private Purse playerPurse;

        private void Start()
        {
            playerPurse = GameObject.FindGameObjectWithTag("Player").GetComponent<Purse>();
            if (playerPurse!=null)
            {
                playerPurse.OnChange += RefreshUI;
            }
            
            RefreshUI();
        }

        private void RefreshUI()
        {
            balanceField.text = $"$ {playerPurse.GetBalance():N2}";
        }
    }
}