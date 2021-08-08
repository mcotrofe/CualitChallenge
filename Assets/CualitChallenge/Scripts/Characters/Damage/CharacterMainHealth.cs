using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMainHealth : Health
{
    private Animator animator;
    private float lastHitTime = -1000;

    private Vector3 lastHitDirecion;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }


    public override Health MainHealth()
    {
        return this;
    }

    public override void ReceiveDamage(int damage, Vector3 direction)
    {
        base.ReceiveDamage(damage, direction);
        lastHitDirecion = direction;
        animator.SetInteger("HitDirection", (int)Mathf.Sign(transform.InverseTransformDirection(direction.normalized).x));
        if(lastHitTime < Time.time + .5f)
            animator.SetTrigger("Hit");
        lastHitTime = Time.time;
    }

    public Vector3 GetLastHitDirection() => lastHitDirecion;
}
