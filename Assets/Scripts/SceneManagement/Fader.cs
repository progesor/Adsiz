﻿using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private Coroutine _currentActiveFade;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            _canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            return Fade(1, time);
        }
        
        public IEnumerator FadeIn(float time)
        {
            return Fade(0, time);
        }

        public IEnumerator Fade(float target, float time)
        {
            if (_currentActiveFade!=null)
            {
                StopCoroutine(_currentActiveFade);
            }
            _currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            yield return _currentActiveFade;
        }

        private IEnumerator FadeRoutine(float target,float time)
        {
            while (!Mathf.Approximately(_canvasGroup.alpha, target))
            {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}