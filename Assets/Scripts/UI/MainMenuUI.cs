using ProgesorCreating.SceneManagement;
using ProgesorCreating.Utils;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private LazyValue<SavingWrapper> _savingWrapper;

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
    }
}