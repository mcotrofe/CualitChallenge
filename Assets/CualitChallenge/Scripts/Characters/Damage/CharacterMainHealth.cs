using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CualitChallenge.Characters.Damage
{
    public class CharacterMainHealth : Health
    {
        [SerializeField] float hitGrazeTime = .5f;
        [SerializeField] UnityEvent onHit;

        public UnityEvent OnHit => onHit;

        private Animator animator;
        private float lastHitTime = 0;

        private Vector3 lastHitDirecion;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }

        public override void ReceiveDamage(int damage, Vector3 direction)
        {
            base.ReceiveDamage(damage, direction);
            lastHitDirecion = direction;
            animator.SetInteger("HitDirection", (int)Mathf.Sign(transform.InverseTransformDirection(direction.normalized).x));
            if (lastHitTime + hitGrazeTime < Time.time )
            {
                lastHitTime = Time.time;
                animator.SetTrigger("Hit");
                GetComponent<CharacterCombat>().OnWeaponSwingEnd();
                onHit.Invoke();
            }
            
        }

        public Vector3 GetLastHitDirection() => lastHitDirecion;

    }

}