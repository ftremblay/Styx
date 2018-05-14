using System.Collections.Generic;
using UnityEngine;
using PoolType = Assets._Scripts.Utils.Constants.PoolType;

namespace Assets._Scripts.Utils.Pooler
{
    public class ObjectPooler : MonoBehaviour
    {
        public List<Pool> _pools;
        private Dictionary<PoolType, Queue<GameObject>> _poolDictionary;

        #region Singleton

        public static ObjectPooler Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        public void Start()
        {
            _poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();

            foreach (Pool pool in _pools)
            {
                var objectPool = new Queue<GameObject>();

                for(var i = 0; i < pool.Size; i++)
                {
                    var obj = Instantiate(pool.Prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                _poolDictionary.Add(pool.Type, objectPool);
            }
        }

        public void SpawnFromPool(PoolType type, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(type))
            {
                Debug.LogWarning("Undefined tag in pool dictionary");
                return;
            }

            var objectToSpawn = _poolDictionary[type].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            var pooledObject = objectToSpawn.GetComponent<IPooledObject>();
            if (pooledObject != null)
            {
                pooledObject.OnSpawn();
            }

            _poolDictionary[type].Enqueue(objectToSpawn);
        }
    }
}
