using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    static readonly string OpenParameter = "Open";
    private Animator animator;

    void Awake() => animator = GetComponent<Animator>();

    public void Open() => animator.SetBool(OpenParameter, true);

    public void Close() => animator.SetBool(OpenParameter, false);



}
