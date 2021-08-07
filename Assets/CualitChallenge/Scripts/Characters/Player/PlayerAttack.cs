using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : CharacterAttack
{
    static readonly string AttackInput = "Attack";

    void Update()
    {
        if (Input.GetButtonDown(AttackInput)) Attack();
    }

}
