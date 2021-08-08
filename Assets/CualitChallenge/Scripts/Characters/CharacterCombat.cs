using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterCombat : MonoBehaviour
{
    static readonly string AttackTrigger = "Attack";

    [SerializeField] MeleeWeapon weapon;

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack() => animator.SetTrigger(AttackTrigger);

    public void OnWeaponSwingStart() => weapon?.StartSwing();

    public void OnWeaponSwingEnd() => weapon?.EndSwing();

    public void DropWeapon()
    {
        if(weapon) weapon.transform.parent = null;
    }
}
