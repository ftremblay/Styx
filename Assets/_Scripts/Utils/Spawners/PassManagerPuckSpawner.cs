using Assets._Scripts.Utils.Pooler;
using UnityEngine;
using PoolType = Assets._Scripts.Utils.Constants.PoolType;

namespace Assets._Scripts.Utils.Spawners
{
    public class PassManagerPuckSpawner : MonoBehaviour
    {
        public PoolType Pool;

        private ObjectPooler _objectPooler;

        public void Start()
        {
            _objectPooler = ObjectPooler.Instance;
        }

        public void Update()
        {
            
        }
    }
}
