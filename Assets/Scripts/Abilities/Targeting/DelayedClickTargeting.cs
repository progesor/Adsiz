using System.Collections;
using ProgesorCreating.Control;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "Delayed Click Targeting",menuName = "Abilities/Targeting/New Delayed Click Targeting",order = 0)]
    public class DelayedClickTargeting : TargetingStrategy
    {
        [SerializeField] private Texture2D cursorTexture;
        [SerializeField] private Vector2 cursorHotspot;
        
        public override void StartTargeting(GameObject user)
        {
            PlayerController playerController = user.GetComponent<PlayerController>();
            playerController.StartCoroutine(Targeting(user, playerController));
        }

        private IEnumerator Targeting(GameObject user, PlayerController playerController)
        {
            playerController.enabled = false;
            while (true)
            {
                Cursor.SetCursor(cursorTexture,cursorHotspot,CursorMode.Auto);

                if (Input.GetMouseButtonDown(0))
                {
                    yield return new WaitWhile(() => Input.GetMouseButton(0));
                    playerController.enabled = true;
                    yield break;
                }
                yield return null;
            }
        }
    }
}