using ProgesorCreating.RPG.Control;
using ProgesorCreating.RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Cinematic
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject _player;
        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
        }

        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }

        void DisableControl(PlayableDirector pd)
        {
            _player.GetComponent<ActionScheduler>().CancelCurrentAction();
            _player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            _player.GetComponent<PlayerController>().enabled = true;
        }
    }
}