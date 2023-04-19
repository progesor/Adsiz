using UnityEngine;
using UnityEngine.Serialization;

namespace ProgesorCreating.RPG.Control
{
    [System.Serializable]
    public struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }
}