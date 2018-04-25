using UnityEngine;

namespace Assets._Scripts.Utils.Pooler
{
    [CreateAssetMenu(fileName = "Pool", menuName = "Pools/New Pool")]
    public class Pool : ScriptableObject
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
    }
}
