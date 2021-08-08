using UnityEngine;
using CualitChallenge.Characters.Damage;

namespace CualitChallenge.Characters
{

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
            //TODO: if (weapon) weapon.transform.parent = null;
            if (weapon) weapon.gameObject.SetActive(false);
        }
        public void RespawnWeapon()
        {
            //TODO: revert this if (weapon) weapon.transform.parent = null;
            if (weapon) weapon.gameObject.SetActive(true);
        }
    }
}
