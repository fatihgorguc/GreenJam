using System;
using System.Collections.Generic;
using _SCRIPTS.Signals;
using UnityEngine;
using UnityEngine.Rendering;

namespace _SCRIPTS.ObjectPooler
{
    public class ObjectPooler : MonoBehaviour
    {
        #region Public Field

        public Dictionary<string, Queue<GameObject>> PoolDictionary;

        #endregion

        #region Serialize Field

        public List<Pool> pools;

        #endregion

        #region OnEnable, Start

        private void OnEnable()
        {
            SubscribeEvents();
        }

        void Start()
        {
            AddEnemiesToQueue();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnSpawnFromPool += OnSpawnFromPool;
        }

        private void AddEnemiesToQueue()
        {
            PoolDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.Size; i++)
                {
                    GameObject obj = Instantiate(pool.Prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                
                PoolDictionary.Add(pool.PoolTag,objectPool);

            }
        }

        private GameObject OnSpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!PoolDictionary.ContainsKey(tag))
            {
                return null;
            }
            
            
            GameObject objectToSpawn = PoolDictionary[tag].Dequeue();
            
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.SetActive(true);
            
            PoolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        #endregion
        
        
    
    }
}
