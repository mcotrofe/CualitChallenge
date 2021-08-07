using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPartHealth : Health
{

    [SerializeField] CharacterMainHealth mainHealth;


    public override Health MainHealth() => mainHealth;

    public override void ReceiveDamage(int damage, Vector3 direction)
    {
        base.ReceiveDamage(damage, direction);
        mainHealth.ReceiveDamage(damage, direction);
    }
}
