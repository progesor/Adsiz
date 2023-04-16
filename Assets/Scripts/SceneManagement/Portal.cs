using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad = -1;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GameObject().CompareTag("Player"))
            {
                
            }
        }

        private IEnumerator Transition()
        {
            SceneManager.LoadScene(sceneToLoad);
            yield return null;
        }
    }
}