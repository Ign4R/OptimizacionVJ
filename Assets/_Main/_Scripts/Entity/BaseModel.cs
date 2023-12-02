using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseModel : Updateable
{

    [SerializeField]
    private float _speed;
    [SerializeField]
    protected Transform _origin;
    protected int _bulletType;
    private Collider _coll;

    public float Speed { get => _speed; private set => _speed = value; }

    private int _layIgnoreEntity;

    public Rigidbody Rb { get ; private set ; }
  
    public virtual void Awake()
    {
        _layIgnoreEntity = LayerMask.NameToLayer("IgnoreEntity");
        Rb = GetComponent<Rigidbody>();
    }
    public void MoveAndRotate(Vector3 dir, Vector3 rotDir)
    {
        dir.y = 0;
        Vector3 dirSpeed = dir * Speed;
        dirSpeed.y = Rb.velocity.y;
        Rb.velocity = dirSpeed;
        Rb.rotation = Quaternion.LookRotation(rotDir);
    }
        
    public virtual void GetDir(Vector3 dir)
    {
        transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    public void Shoot(ObjectPool objectPool)
    {
        var bullet = objectPool.GetPooledObject();
        bullet.transform.SetPositionAndRotation(_origin.position, _origin.rotation);
        bullet.gameObject.layer = _bulletType;
    }
    public IEnumerator NotCollisionEntity()
    {
        int myLayer = gameObject.layer;
        gameObject.layer = _layIgnoreEntity; //layer que no detecta collision con alguna entidad
        yield return new WaitForSeconds(2f); // Ajusta el tiempo según sea necesario
        gameObject.layer = myLayer;
    }
}
