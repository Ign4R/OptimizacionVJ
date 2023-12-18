using UnityEngine;
public static class OnCollision
{
    public static bool NonAlloc(Vector3 position, float radius, Collider[] colls, int layerColls, int count = 0)
    {
        return Physics.OverlapSphereNonAlloc(position, radius, colls, layerColls) > count;
    }

}


