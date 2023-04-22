using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Utils
{
    /// <summary>
    /// An alternative to using the singleton pattern. Will handle spawning a
    /// prefab only once across multiple scenes.
    ///
    /// Place this in a prefab that exists in every scene. Point to another
    /// prefab that contains all GameObjects that should be singletons. The
    /// class will spawn the prefab only once and set it to persist between
    /// scenes.
    /// </summary>
    public class PersistentObjectSpawner : MonoBehaviour
    {
        // CONFIG DATA
        [Tooltip("This prefab will only be spawned once and persisted between " +
                 "scenes.")]
        [SerializeField] GameObject persistentObjectPrefab;

        // PRIVATE STATE
        static bool _hasSpawned;

        // PRIVATE
        private void Awake() {
            if (_hasSpawned) return;

            SpawnPersistentObjects();

            _hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}