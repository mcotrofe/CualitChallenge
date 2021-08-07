using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public string Tag;
    public UnityEvent TriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag)) TriggerEnter.Invoke();
    }
}
