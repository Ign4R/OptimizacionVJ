using UnityEngine;
public class PlayerModel : BaseModel, IDestroyable
{
    private Vector3 _posSpawn;
    public override void Awake()
    {
        base.Awake();
        _layColls = 1 << LayerMask.NameToLayer("Enemy"); ///Caching :D
    }
    public void Die()
    {
        transform.position = _posSpawn;    
    }
  
    public void SetPosSpawn(Vector3 pos)
    {
        _posSpawn = pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
