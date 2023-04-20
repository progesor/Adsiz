using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.UI.Damage_Text
{
    public class DamageText:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI damageText;
        public void SetValue(float amount)
        {
            damageText.SetText(string.Format("{0:0.0}", amount));
        }
    }
}