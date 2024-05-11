using System;
using System.Collections;
using _SCRIPTS.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _SCRIPTS.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private GameObject[] enemyTypes;
        [SerializeField] private float spawnRadius = 5;
        [SerializeField] private float spawnHeight = 1;
        
        [SerializeField] private float spawnRateMin = 1;
        [SerializeField] private float spawnRateMax = 5;
        [SerializeField] private float spawnRateIncreaseTime = 30;

        private float _currentSpawnRate;
        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        IEnumerator SpawnRoutine()
        {
            if (CoreGameSignals.Instance.OnGetKillCount.Invoke() < 10)
            {
                var randomSpawnTime = Random.Range(spawnRateMin, spawnRateMax);
                yield return new WaitForSeconds(randomSpawnTime);
                var randomSpawnPoint = Random.Range(0, 9);
                var RandomEnemyType = Random.Range(0, 2);
                Instantiate(enemyTypes[RandomEnemyType],
                    spawnPoints[randomSpawnPoint].position,spawnPoints[randomSpawnPoint].rotation);
            }
            else
            {
                int difficultyLevel = CoreGameSignals.Instance.OnGetKillCount.Invoke() / 10;
                var randomSpawnTime = Random.Range(MinRate(difficultyLevel), MaxRate(difficultyLevel));
                yield return new WaitForSeconds(randomSpawnTime);
                var randomSpawnPoint = Random.Range(0, 9);
                var RandomEnemyType = Random.Range(0, 2);
                Instantiate(enemyTypes[RandomEnemyType],
                    spawnPoints[randomSpawnPoint].position,spawnPoints[randomSpawnPoint].rotation);
            }
            StartCoroutine(SpawnRoutine());
        }

        private float MaxRate(int val)
        {
            var maxRate = spawnRateMax;
            maxRate -= maxRate * val / 8;
            return maxRate;
        }
        private float MinRate(int val)
        {
            var minRate = spawnRateMin;
            minRate -= minRate * val / 8;
            return minRate;
        }
    }
}
