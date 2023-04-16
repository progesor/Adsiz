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
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            _player = GameObject.FindWithTag("Player");
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