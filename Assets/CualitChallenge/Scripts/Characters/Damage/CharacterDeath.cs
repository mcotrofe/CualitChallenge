using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterMainHealth))]
public class CharacterDeath : MonoBehaviour
{
    [SerializeField] Rigidbody chestRigidbody;
    [SerializeField] Rigidbody headRigidbody;
    [SerializeField] UnityEvent onDead;


    private bool isDead = false;
    private CharacterMainHealth health;



    void Awake()
    {
        health = GetComponent<CharacterMainHealth>();
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isDead && health.HP() <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        onDead.Invoke();
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
        headRigidbody.velocity = health.GetLastHitDirection().normalized * Random.Range(1, 20);
        chestRigidbody.velocity = health.GetLastHitDirection().normalized * Random.Range(1, 20);
        yield return null;
    }
}
