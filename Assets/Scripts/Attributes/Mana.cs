using System;
using ProgesorCreating.Saving;
using ProgesorCreating.Stats;
using ProgesorCreating.Utils;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Attributes
{
    public class Mana : MonoBehaviour,ISaveable
    {
        private LazyValue<float> _mana;
        private BaseStats _baseStats;

        private void Awake()
        {
            _baseStats = GetComponent<BaseStats>();
            _mana = new LazyValue<float>(GetMaxMana);
        }

        private void Update()
        {
            if (_mana.Value<GetMaxMana())
            {
                _mana.Value += GetRegenRate() * Time.deltaTime;
                if (_mana.Value>GetMaxMana())
                {
                    _mana.Value = GetMaxMana();
                }
            }
        }

        public float GetMana()
        {
            return _mana.Value;
        }

        public float GetMaxMana()
        {
            return _baseStats.GetStat(Stat.Mana);
        }

        public float GetRegenRate()
        {
            return _baseStats.GetStat(Stat.ManaRegenRate);
        }

        public bool UseMana(float manaToUse)
        {
            if (manaToUse>_mana.Value)
            {
                return false;
            }
            _mana.Value -= manaToUse;
            return true;
        }

        public object CaptureState()
        {
            return _mana.Value;
        }

        public void RestoreState(object state)
        {
            _mana.Value = (float)state;
        }
    }
}