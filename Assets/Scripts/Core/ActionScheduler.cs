﻿using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction _currentAction;

        public void StartAction(IAction action)
        {
            if (_currentAction==action)return;
            
            if (_currentAction!=null)
            {
                _currentAction.Cancel();
            }
            _currentAction = action;
        }
        
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
    
}