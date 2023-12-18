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
        Debug.LogError("DieEnemy");
        GameManager.Instance.EnemyPool.ReturnToPool(gameObject);
        GameManager.Instance.CheckIfPoolNotEmpty();
        GameManager.CounterEntity();
    }
}
