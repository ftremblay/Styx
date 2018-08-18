namespace RageCure.Commons.Utils

module Vector3Utils =
    open UnityEngine

    let normalizeIfNot (vector: Vector3) =
        if vector.magnitude > 1.f then
            vector.normalized
        else
            vector

    let normalize (vector: Vector3) =
        vector.normalized

    let projectOnPlane (normal: Vector3) (vector: Vector3) =
        Vector3.ProjectOnPlane (vector, normal)

    let isNear (pos1: Vector3) (pos2: Vector3) =
        let ratioX = pos1.x - pos2.x
        let ratioY = pos1.y - pos2.y
        let ratioZ = pos1.z - pos2.z
        (ratioX <= 0.2f && ratioY <= 0.2f && ratioZ <= 0.2f)
    