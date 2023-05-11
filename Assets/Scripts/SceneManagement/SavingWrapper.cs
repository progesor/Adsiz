using System.Collections;
using ProgesorCreating.Saving;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] KeyCode saveKey = KeyCode.S;
        [SerializeField] KeyCode loadKey = KeyCode.L;
        [SerializeField] KeyCode deleteKey = KeyCode.Delete;
        private const string DefaultSaveFile = "save";

        [SerializeField] private float fadeInTime = 0.2f;
        [SerializeField] private float fadeOutTime = 0.2f;

        public void ContinueGame()
        {
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return GetComponent<SavingSystem>().LoadLastScene(DefaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(saveKey))
            {
                Save();
            }
            if (Input.GetKeyDown(loadKey))
            {
                Load();
            }
            if (Input.GetKeyDown(deleteKey))
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