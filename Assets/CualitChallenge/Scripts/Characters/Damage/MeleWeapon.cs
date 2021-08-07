using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleWeapon : MonoBehaviour
{
    [SerializeField] Transform[] raycastPoints;
    [SerializeField] UnityEvent OnWeaponSwingStart;
    [SerializeField] UnityEvent OnWeaponSwingEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartSwing()
    {
        OnWeaponSwingStart.Invoke();
    }

    public void EndSwing()
    {
        OnWeaponSwingEnd.Invoke();
    }
}
