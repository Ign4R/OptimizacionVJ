using System;
using UnityEngine;

public class PlayerModel : BaseModel
{
    public override void Awake()
    {
        base.Awake();
        _bulletType = LayerMask.NameToLayer("BulletPlayer");
    }
    public void Respawn(Vector3 pos)
    {
        transform.position = pos;
    }

}
