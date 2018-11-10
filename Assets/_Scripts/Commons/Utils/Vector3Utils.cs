using UnityEngine;

namespace Styx.Commons.Utils
{
    public class Vector3Utils
    {
        public static Vector3 NormalizeIfNot (Vector3 vector)
        {
            if (vector.magnitude > 1f || vector.magnitude < 1f)
                return vector.normalized;
            return vector;
        }

        public static Vector3 NormalizeIfGreater (Vector3 vector)
        {
            if (vector.magnitude > 1f)
                return vector.normalized;
            return vector;
        }
    }
}
