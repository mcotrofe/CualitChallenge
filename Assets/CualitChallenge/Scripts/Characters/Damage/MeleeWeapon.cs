using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CualitChallenge.Characters.Damage
{
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] int damage = 1;
        [SerializeField] LayerMask hitLayers;
        [SerializeField] bool filterByTag = false;
        [SerializeField] string hitTag;
        [SerializeField] Transform[] raycastPoints;

        [SerializeField] UnityEvent OnWeaponSwingStart;
        [SerializeField] UnityEvent OnWeaponSwingEnd;
        [SerializeField] bool DrawDebug = false;

        private bool isDetectionActive = false;
        private Vector3[] prevPositions;
        private List<Collider> processedColliders = new List<Collider>();
        private CharacterMainHealth wielderHealth;


        private void Awake()
        {
            wielderHealth = GetComponentInParent<CharacterMainHealth>();
        }

        public void StartSwing()
        {
            OnWeaponSwingStart.Invoke();
            prevPositions = null;
            isDetectionActive = true;
            processedColliders.Clear();
        }

        public void EndSwing()
        {
            OnWeaponSwingEnd.Invoke();
            isDetectionActive = false;
        }


        void FixedUpdate()
        {
            if (isDetectionActive)
            {
                if (prevPositions == null) ReadInitialPositions();
                else UpdateHitDetection();
            }
        }

        private void ReadInitialPositions()
        {
            prevPositions = new Vector3[raycastPoints.Length];
            for (int i = 0; i < raycastPoints.Length; i++)
            {
                prevPositions[i] = raycastPoints[i].position;
            }
        }

        private void UpdateHitDetection()
        {
            for (int i = 0; i < raycastPoints.Length; i++)
            {
                UpdatePointDetection(i);
            }
        }

        private void UpdatePointDetection(int pointIndex)
        {
            Vector3 currentPos = raycastPoints[pointIndex].position;
            Vector3 lastPosition = prevPositions[pointIndex];
            Vector3 direction = currentPos - lastPosition;

            RaycastHit hitInfo;
            bool hit = Physics.Raycast(lastPosition, direction, out hitInfo, direction.magnitude, hitLayers, QueryTriggerInteraction.Collide);
            if (hit && !processedColliders.Contains(hitInfo.collider) && IsCorrectTag(hitInfo.collider))
            {
                processedColliders.Add(hitInfo.collider);
                var health = hitInfo.collider.GetComponent<Health>();
                if (health && health != wielderHealth) health.ReceiveDamage(damage, direction);
            }
            prevPositions[pointIndex] = currentPos;

            if (DrawDebug) Debug.DrawRay(lastPosition, direction, hit ? Color.red : Color.yellow, hit ? 1.5f : .5f);

        }

        private bool IsCorrectTag(Collider collider) => !filterByTag || collider.CompareTag(hitTag);
    }
}