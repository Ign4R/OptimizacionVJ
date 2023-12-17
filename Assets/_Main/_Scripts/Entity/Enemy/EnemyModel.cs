using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : BaseModel, IDestroyable
{
    public override void Awake()
    {
        base.Awake();
        _bulletType = LayerMask.NameToLayer("BulletEnemy");
    }

    public void Die()
    {
        GameManager.EnemyPool.ReturnToPool(gameObject);
        GameManager.CounterEntity();
        GameManager.Instance?.CheckIfPoolNotEmpty();
        print("Die Enemy");
    }
}
