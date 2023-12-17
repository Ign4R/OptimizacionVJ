using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerModel : BaseModel, IDestroyable
{
    private Vector3 _posSpawn;
    public override void Awake()
    {
        base.Awake();
        _bulletType = LayerMask.NameToLayer("BulletPlayer");
    }

    public void Die()
    {
        print("JUGADOR MUERTO");
        gameObject.SetActive(false);
        //StartCoroutine(RespawnImmunity());
        //_destroyed = false;
        //Rb.velocity = Vector3.zero;
        //gameObject.SetActive(false);
        //transform.position = _posSpawn;
    }
  
    public void SetPosSpawn(Vector3 pos)
    {
        _posSpawn = pos;
    }


}
