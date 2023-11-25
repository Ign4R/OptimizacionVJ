using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Updateable, IDestroyable
{
    [SerializeField]
    private int layerCollision;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == layerCollision)
        {
            Die();
        }
    }
    public void Die()
    {
        ///TODO: HACER POOL
        print("Die Enemy");
    }

}
