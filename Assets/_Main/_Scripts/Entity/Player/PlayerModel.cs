using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerModel : BaseModel, IDestroyable
{
    private Vector3 _posSpawn;


    public override void Awake()
    {
        base.Awake();
        _layColls = 1 << LayerMask.NameToLayer("Enemy");
        ///Caching :D
        _bulletType = LayerMask.NameToLayer("BulletPlayer");
    }
    public void Die()
    {
        Debug.Log("Die Player");
        transform.position = _posSpawn;
     
    }
  
    public void SetPosSpawn(Vector3 pos)
    {
        _posSpawn = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
