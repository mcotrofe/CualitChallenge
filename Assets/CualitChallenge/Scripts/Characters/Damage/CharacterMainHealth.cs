using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMainHealth : Health
{


    public override Health MainHealth()
    {
        return this;
    }

    public override void ReceiveDamage(int damage, Vector3 direction)
    {
        base.ReceiveDamage(damage, direction);
    }
}
