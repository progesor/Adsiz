using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float areaAffectRadius;
        [SerializeField] private Transform targetingPrefab;

        private Transform _targetingPrefabInstance;
        
        public override void StartTargeting(AbilityData data,Action finished)
        {
            PlayerController playerController = data.GetUser().GetComponent<PlayerController>();
            playerController.StartCoroutine(Targeting(data, playerController,finished));
        }

        private IEnumerator Targeting(AbilityData data, PlayerController playerController,Action finished)
        {
            playerController.enabled = false;
            if (_targetingPrefabInstance == null)
            {
                _targetingPrefabInstance = Instantiate(targetingPrefab);
            }
            else
            {
                _targetingPrefabInstance.gameObject.SetActive(true);
            }

            _targetingPrefabInstance.localScale = new Vector3(areaAffectRadius * 2, 1, areaAffectRadius * 2);

            while (!data.IsCancelled())
            {
                Cursor.SetCursor(cursorTexture,cursorHotspot,CursorMode.Auto);
                RaycastHit raycastHit;
                if (Physics.Raycast(PlayerController.GetMouseRay(), out raycastHit, 1000, layerMask))
                {
                    _targetingPrefabInstance.position = raycastHit.point;
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        yield return new WaitWhile(() => Input.GetMouseButton(0));
                        data.SetTargetedPoint(raycastHit.point);
                        data.SetTargets(GetGameObjectsInRadius(raycastHit.point));
                        break;
                    }
                }
                yield return null;
            }
            _targetingPrefabInstance.gameObject.SetActive(false);
            playerController.enabled = true;
            finished();
        }

        private IEnumerable<GameObject> GetGameObjectsInRadius(Vector3 point)
        {

            RaycastHit[] hits = Physics.SphereCastAll(point, areaAffectRadius, Vector3.up, 0);
            foreach (RaycastHit hit in hits)
            {
                yield return hit.collider.gameObject;
            }
        }
    }
}