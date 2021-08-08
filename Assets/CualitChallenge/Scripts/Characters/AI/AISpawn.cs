using UnityEngine;
using UnityEngine.Events;
using CualitChallenge.Characters.Damage;

namespace CualitChallenge.Characters.AI
{
    [RequireComponent(typeof(CharacterMainHealth))]
    public class AISpawn : MonoBehaviour
    {
        [SerializeField] UnityEvent onSpawn;


        public void Spawn()
        {
            onSpawn.Invoke();
        }

    }

}