using System;
using System.Collections;
using _SCRIPTS.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _SCRIPTS.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        #region Serialize Field
        [SerializeField] private Transform[] spawnPoints;
        
        [SerializeField] private float spawnRateMin = 1;
        [SerializeField] private float spawnRateMax = 5;
        #endregion

        #region Private Field

        private float _currentSpawnRate;
        private CoreGameSignals _coreGameSignals;

        #endregion

        #region Awake, Start

        private void Awake()
        {
            _coreGameSignals = CoreGameSignals.Instance;
        }

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        #endregion

        #region Function

        private IEnumerator SpawnRoutine()
        {
            if (CoreGameSignals.Instance.OnGetKillCount.Invoke() < 10)
            {
                var randomSpawnTime = Random.Range(spawnRateMin, spawnRateMax);
                yield return new WaitForSeconds(randomSpawnTime);
                Spawner();
            }
            else
            {
                int difficultyLevel = CoreGameSignals.Instance.OnGetKillCount.Invoke() / 10;
                var randomSpawnTime = Random.Range(MinRate(difficultyLevel), MaxRate(difficultyLevel));
                yield return new WaitForSeconds(randomSpawnTime);
                Spawner();
            }
            StartCoroutine(SpawnRoutine());
        }

        private void Spawner()
        {
            var randomSpawnPoint = Random.Range(0, 9);
            var randomEnemyType = Random.Range(0, 3);
            while(true)
            {
                if (spawnPoints[randomSpawnPoint].position.x > 83 || spawnPoints[randomSpawnPoint].position.x < -70 ||
                    spawnPoints[randomSpawnPoint].position.z > 56 || spawnPoints[randomSpawnPoint].position.z < -56)
                {
                    randomSpawnPoint = Random.Range(0, 9);
                }
                else
                {
                    break;
                }
            }

            if (randomEnemyType ==0)
            {
                _coreGameSignals.OnSpawnFromPool?.Invoke("Green Enemy", spawnPoints[randomSpawnPoint].position,
                    spawnPoints[randomSpawnPoint].rotation);
            }
            else
            {
                _coreGameSignals.OnSpawnFromPool?.Invoke("Ranged Enemy", spawnPoints[randomSpawnPoint].position,
                    spawnPoints[randomSpawnPoint].rotation);
            }
        }

        private float MaxRate(int val)
        {
            var maxRate = spawnRateMax;
            maxRate -= maxRate * val / 10;
            return maxRate;
        }
        private float MinRate(int val)
        {
            var minRate = spawnRateMin;
            minRate -= minRate * val / 10;
            return minRate;
        }

        #endregion


    }
}
