using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Core
{
    public class CameraFacing : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}