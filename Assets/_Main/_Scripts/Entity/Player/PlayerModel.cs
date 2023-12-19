using UnityEngine;
public class PlayerModel : BaseModel, IDestroyable
{
    private Vector3 _posSpawn;
    public override void Initialization()
    {
        base.Initialization();
        _layColls = 1 << LayerMask.NameToLayer("Enemy"); ///Caching :D
    }
    public void Die(bool dieForBullet = false)
    {
        transform.position = _posSpawn;
        transform.rotation = Quaternion.identity;
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
