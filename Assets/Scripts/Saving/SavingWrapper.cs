using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string defaultSaveFile = "save";

        private IEnumerator Start()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}