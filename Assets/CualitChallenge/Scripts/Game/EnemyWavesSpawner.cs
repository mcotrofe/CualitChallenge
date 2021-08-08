using UnityEngine;
using CualitChallenge.Characters.AI;
using CualitChallenge.Characters;
using System.Collections;
using System.Collections.Generic;

namespace CualitChallenge.Game
{

    public class EnemyWavesSpawner : MonoBehaviour
    {
        [SerializeField] Transform enemyPoolTransform;
        [SerializeField] Transform spawnPointsParent;

        private Stack<GameObject> enemyPool = new Stack<GameObject>();
        private List<GameObject> spawnedEnemies = new List<GameObject>();
        private int livingEnemies = 0;
        
        void Start()
        {
            InitializeQueue();
        }

        private void InitializeQueue()
        {
            for (int i = enemyPoolTransform.childCount - 1; i >= 0; i--)
            {
                GameObject child = enemyPoolTransform.GetChild(i).gameObject;
                enemyPool.Push(child);
                child.SetActive(false);
                child.GetComponent<CharacterDeath>().OnDeath.AddListener(EnemyDeath);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartWave(int currentWave)
        {
            StartCoroutine(WaveCoroutine(currentWave));
        }

        private void SpawnNext()
        {
            GameObject newEnemy = enemyPool.Pop();
            Transform spawnPoint = RandomSpawnPoint();
            newEnemy.transform.parent = null;
            newEnemy.transform.position = spawnPoint.transform.position;
            newEnemy.transform.rotation = spawnPoint.transform.rotation;


            newEnemy.SetActive(true);
            newEnemy.GetComponent<AISpawn>().Spawn();
            spawnedEnemies.Add(newEnemy);
            livingEnemies++;
        }

        private Transform RandomSpawnPoint() => spawnPointsParent.GetChild(UnityEngine.Random.Range(0, spawnPointsParent.childCount));

        private void EnemyDeath() => livingEnemies--;


        private IEnumerator WaveCoroutine(int currentWave)
        {
            yield return new WaitForSeconds(3);
            int enemyCount = 0;
            int waveCount = 0;

            while (waveCount < 4 + currentWave && enemyPool.Count > 0) {
                enemyCount = 0;
                while (enemyCount < 1 + waveCount * (currentWave  + 1) && enemyPool.Count > 0)
                {
                    SpawnNext();
                    enemyCount++;
                    yield return new WaitForSeconds(Random.Range(1, 2f));
                }

                while (livingEnemies > 0) yield return null;
                waveCount++;
                yield return new WaitForSeconds(Random.Range(2, 3));
            }
            while (livingEnemies > 0) yield return null;
            print("WAVE ENDED");
            yield return null;
        }

    }
}
