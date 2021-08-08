using System.Collections.Generic;
using UnityEngine;

namespace CualitChallenge.Game
{

    public class EnemyWavesSpawner : MonoBehaviour
    {
        [SerializeField] Transform enemyPoolTransform;

        private Queue<GameObject> enemyPool = new Queue<GameObject>();

        void Start()
        {
            InitializeQueue();
        }

        private void InitializeQueue()
        {
            foreach (Transform child in enemyPoolTransform)
            {
                enemyPool.Enqueue(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
