using UnityEngine;

namespace CualitChallenge.Characters.Damage
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHP = 5;

        private int hp;

        protected virtual void Awake()
        {
            hp = maxHP;
        }

        public virtual void ReceiveDamage(int damage, Vector3 direction)
        {
            hp -= damage;
        }

        public int HP() => hp;
        public int MaxHP() => maxHP;

        public void Heal()
        {
            hp = maxHP;
        }

    }
}