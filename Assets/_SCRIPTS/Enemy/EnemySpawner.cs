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
        
    }
}
