using ProgesorCreating.SceneManagement;
using ProgesorCreating.Utils;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private LazyValue<SavingWrapper> _savingWrapper;

        [SerializeField] private TMP_InputField newGameNameField;

        private void Awake()
        {
            _savingWrapper = new LazyValue<SavingWrapper>(GetSavingWrapper);
        }

        private SavingWrapper GetSavingWrapper()
        {
            return FindObjectOfType<SavingWrapper>();
        }

        public void ContinueGame()
        {
            _savingWrapper.Value.ContinueGame();
        }

        public void NewGame ()
        {
            _savingWrapper.Value.NewGame(newGameNameField.text);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}