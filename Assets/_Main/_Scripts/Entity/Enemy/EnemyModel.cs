using UnityEngine;
public class EnemyModel : BaseModel, IDestroyable
{
    public override void Awake()
    {
        base.Awake();
        _layColls = 1 << LayerMask.NameToLayer("Enemy") | (1 << LayerMask.NameToLayer("Player"));
    }
    public Vector3 GetRandomDir()
    {
        int randomInt = Random.Range(0, 4);
        switch (randomInt)
        {
            case 0:
                return Vector3.forward;
            case 1:
                return Vector3.right;
            case 2:
                return Vector3.back;
            case 3:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }
    public void Die()
    {
        GameManager.Instance.EnemyPool.ReturnToPool(this);
        GameManager.Instance.CheckIfPoolNotEmpty();
        GameManager.CounterEntity();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
