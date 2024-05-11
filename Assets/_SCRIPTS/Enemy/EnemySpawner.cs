using UnityEngine;

namespace _SCRIPTS.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnRadius = 5;
        [SerializeField] private float spawnHeight = 1;
        
        [SerializeField] private float spawnRateMax = 1;
        [SerializeField] private float spawnRateAtStart = 1;
        
        private float _spawnTimer;
        
        private void Update()
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= spawnRate)
            {
                SpawnEnemy();
                _spawnTimer = 0;
            }
        }

        private void SpawnEnemy()
        {
            var spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = spawnHeight;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
