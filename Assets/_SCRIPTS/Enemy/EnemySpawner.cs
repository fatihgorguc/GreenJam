using System;
using System.Collections;
using UnityEngine;

namespace _SCRIPTS.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnRadius = 5;
        [SerializeField] private float spawnHeight = 1;
        
        [SerializeField] private float spawnRateMin = 0.3f;
        [SerializeField] private float spawnRateMax = 3;
        [SerializeField] private float spawnRateIncreaseTime = 30;

        private float _currentSpawnRate;
        private void Start()
        {
            
        }

        IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(spawnRateMin + (spawnRateMax - spawnRateMin));
            if (_currentSpawnRate < spawnRateIncreaseTime) _currentSpawnRate++;
        }
    }
}
