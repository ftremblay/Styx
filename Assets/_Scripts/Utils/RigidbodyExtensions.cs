using UnityEngine;

namespace Assets._Scripts.Utils
{
    public static class RigidbodyExtensions
    {
        public static void SetPosition(this Rigidbody rigidbody, Vector3 position)
        {
            rigidbody.gameObject.transform.SetPositionAndRotation(position, Quaternion.identity);
        }
    }
}
