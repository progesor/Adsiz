﻿using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        public abstract void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> finished);
    }
}