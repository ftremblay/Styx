using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Utils.Pooler
{
    public class ObjectPooler : MonoBehaviour
    {
        public List<Pool> _pools;
        private Dictionary<string, Queue<GameObject>> _poolDictionary;

        #region Singleton

        public static ObjectPooler Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        public void Start()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in _pools)
            {
                var objectPool = new Queue<GameObject>();

                for(var i = 0; i < pool.Size; i++)
                {
                    var obj = Instantiate(pool.Prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                _poolDictionary.Add(pool.Tag, objectPool);
            }
        }

        public void SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Undefined tag in pool dictionary");
                return;
            }

            var objectToSpawn = _poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            var pooledObject = objectToSpawn.GetComponent<IPooledObject>();
            if (pooledObject != null)
            {
                pooledObject.OnSpawn();
            }

            _poolDictionary[tag].Enqueue(objectToSpawn);
        }
    }
}
