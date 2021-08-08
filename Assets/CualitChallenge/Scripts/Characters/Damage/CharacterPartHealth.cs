using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CualitChallenge.Characters.Damage
{
    public class CharacterPartHealth : Health
    {

        [SerializeField] CharacterMainHealth mainHealth;

        public override void ReceiveDamage(int damage, Vector3 direction)
        {
            base.ReceiveDamage(damage, direction);
        }
    }
}