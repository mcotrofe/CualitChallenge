using UnityEngine;
using CualitChallenge.Characters;

namespace CualitChallenge.Characters.Player
{

    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerCombat : CharacterCombat
    {
        static readonly string AttackInput = "Attack";

        private bool inCombatArea = false;

        private PlayerMovement movement;

        private float lastAttackTime = -1000;


        protected override void Awake()
        {
            base.Awake();
            movement = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            if (Input.GetButtonDown(AttackInput))
            {
                lastAttackTime = Time.time;
                Attack();
            }

            movement.SetInCombat(inCombatArea || Time.time < lastAttackTime + 4);
           
        }

        public void SetInCombatArea(bool InCombatArea) => inCombatArea = InCombatArea;

    }
}