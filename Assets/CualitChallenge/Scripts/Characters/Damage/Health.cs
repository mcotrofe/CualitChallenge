using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{


    public virtual Health MainHealth()
    {
        return this;
    }

    public virtual void ReceiveDamage(int damage)
    {

    }
}
