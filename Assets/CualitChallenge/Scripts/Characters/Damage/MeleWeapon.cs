using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleWeapon : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] LayerMask hitLayers;
    [SerializeField] Transform[] raycastPoints;
    [SerializeField] UnityEvent OnWeaponSwingStart;
    [SerializeField] UnityEvent OnWeaponSwingEnd;

    private bool isDetectionActive = false;
    private Vector3[] prevPositions;
    private List<Collider> processedColliders = new List<Collider>();
    private List<Health> charactersHit = new List<Health>();


    public void StartSwing()
    {
        OnWeaponSwingStart.Invoke();
        prevPositions = null;
        isDetectionActive = true;
        processedColliders.Clear();
        charactersHit.Clear();
    }

    public void EndSwing()
    {
        OnWeaponSwingEnd.Invoke();
        isDetectionActive = false;
    }


    void Update()
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
        Transform point = raycastPoints[pointIndex];
        Vector3 currentPos = point.position;
        Vector3 lastPosition = prevPositions[pointIndex];
        Vector3 direction = currentPos - lastPosition;
        float distance = direction.magnitude;
        float speed = distance / Time.deltaTime;
        Vector3 velocity = direction.normalized * speed;

        RaycastHit hit;
        if (Physics.Raycast(lastPosition, direction, out hit, distance, hitLayers, QueryTriggerInteraction.Collide) && !processedColliders.Contains(hit.collider))
        {
            processedColliders.Add(hit.collider);
            var health = hit.collider.GetComponent<Health>();
            if (health)
            {
                if (charactersHit.Contains(health.MainHealth())) return;
                charactersHit.Add(health.MainHealth());
                health.ReceiveDamage(damage);
            }
        }
        prevPositions[pointIndex] = currentPos;
    }





}
