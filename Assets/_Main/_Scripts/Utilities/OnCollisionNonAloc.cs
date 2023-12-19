using UnityEngine;
public class OnCollisionNonAloc
{
    public float Radius { get; private set; }
    public Collider[] Colls { get; private set; }
    public int LayColls { get; private set; }

    public OnCollisionNonAloc(float radius, Collider[] colls)
    {
        Radius = radius; 
        Colls = colls;
    }
    public bool Sphere(Vector3 pos, int? layColls = null, int contacs = 0)
    {
        /// "-1" representa la convencion de "sin capa especifica"
        return Physics.OverlapSphereNonAlloc(pos, Radius, Colls, layColls ?? -1) > contacs;
    }

}