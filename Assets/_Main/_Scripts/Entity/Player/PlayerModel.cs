using System;
using UnityEngine;

public class PlayerModel : BaseModel
{
    private int _myLayer;
    public override void Awake()
    {
        base.Awake();
        _bulletType = LayerMask.NameToLayer("BulletPlayer");
        _myLayer = LayerMask.NameToLayer("Player");
    }


    public void Respawn(Vector3 pos)
    {
        Rb.velocity = Vector3.zero;
        StartCoroutine(NotCollisionEntity(_myLayer));
        transform.position = pos;
    }

}
