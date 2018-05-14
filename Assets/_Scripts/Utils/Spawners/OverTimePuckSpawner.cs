using Assets._Scripts.Utils.Pooler;
using UnityEngine;

namespace Assets._Scripts.Utils.Spawners
{
    public class OverTimePuckSpawner : MonoBehaviour
    {
        public float Cooldown;
        public Constants.PoolType Pool;

        private ObjectPooler _objectPooler;
        private float _timestamp;

        public void Start()
        {
            _objectPooler = ObjectPooler.Instance;
        }

        public void FixedUpdate()
        {
            if (_timestamp > Time.time)
                return;

            _objectPooler.SpawnFromPool(Pool, transform.position, transform.rotation);
            _timestamp = Time.time + Cooldown;
        }
    }
}
