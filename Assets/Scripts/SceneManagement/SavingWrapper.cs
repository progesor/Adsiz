using System.Collections;
using ProgesorCreating.RPG.Saving;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string DefaultSaveFile = "save";

        [SerializeField] private float fadeInTime = 0.2f;

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(DefaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeInTime);
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
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(DefaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(DefaultSaveFile);
        }

        private void Delete()
        {
            GetComponent<SavingSystem>().Delete(DefaultSaveFile);
        }
    }
}