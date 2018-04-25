using Assets._Scripts.Utils.Pooler;
using UnityEngine;

namespace Assets._Scripts.Utils.Spawners
{
    public class OverTimePuckSpawner : MonoBehaviour
    {
        public float Cooldown;

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

            _objectPooler.SpawnFromPool("Puck", transform.position, Quaternion.identity);
            _timestamp = Time.time + Cooldown;
        }
    }
}
