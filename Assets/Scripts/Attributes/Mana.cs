using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Attributes
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] private float maxMana = 200;
        [SerializeField] private float manaRegenRate = 5;

        private float _mana;

        private void Awake()
        {
            _mana = maxMana;
        }

        private void Update()
        {
            if (_mana<maxMana)
            {
                _mana += manaRegenRate * Time.deltaTime;
                if (_mana>maxMana)
                {
                    _mana = maxMana;
                }
            }
        }

        public float GetMana()
        {
            return _mana;
        }

        public float GetMaxMana()
        {
            return maxMana;
        }

        public bool UseMana(float manaToUse)
        {
            if (manaToUse>_mana)
            {
                return false;
            }
            _mana -= manaToUse;
            return true;
        }
    }
}