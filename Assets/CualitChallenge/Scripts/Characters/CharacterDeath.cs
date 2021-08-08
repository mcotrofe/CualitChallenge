using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using CualitChallenge.Characters.Damage;

namespace CualitChallenge.Characters
{

    [RequireComponent(typeof(CharacterMainHealth))]
    public class CharacterDeath : MonoBehaviour
    {
        [SerializeField] Rigidbody chestRigidbody;
        [SerializeField] Rigidbody headRigidbody;
        [SerializeField] GameObject[] deadEffects;
        [SerializeField] UnityEvent onDeath;
        
        public UnityEvent OnDeath => onDeath;

        private bool isDead = false;
        private CharacterMainHealth health;


        void Awake()
        {
            health = GetComponent<CharacterMainHealth>();
            ResetCharacter();

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!isDead && health.HP() <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            isDead = true;
            onDeath.Invoke();
            StartCoroutine(DieCoroutine());

        }

        private IEnumerator DieCoroutine()
        {
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
            }
            GetComponent<Animator>().enabled = false;
            GetComponent<CharacterCombat>().DropWeapon();
            yield return null;
            headRigidbody.velocity = (health.GetLastHitDirection().normalized + Vector3.up * .15f) * Random.Range(2, 15);
            chestRigidbody.velocity = health.GetLastHitDirection().normalized * Random.Range(2, 15);
            PlayFx();
            yield return null;
        }

        public void ResetCharacter()
        {
            health.Heal();
            isDead = false;
            foreach (GameObject fx in deadEffects) fx.SetActive(false);
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = true;
            }
            GetComponent<Animator>().enabled = true;
            GetComponent<CharacterCombat>().RespawnWeapon();
        }

        private void PlayFx()
        {
            foreach (GameObject fx in deadEffects)
            {
                fx.SetActive(true);
                ParticleSystem ps = fx.GetComponent<ParticleSystem>();
                ps?.Play();
            }
        }
    }
}