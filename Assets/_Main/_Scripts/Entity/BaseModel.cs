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
    protected bool _destroyed;

    private Collider[] _colls = new Collider[3];


    public float Speed { get => _speed; private set => _speed = value; }

    private int _layIgnoreEntity;

    public Rigidbody Rb { get ; private set ; }
    public Collider[] Colls { get => _colls; set => _colls = value; }

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
    public bool CollisionNonAlloc(float radius, int layerTarget)
    {
        bool hitCount = Physics.OverlapSphereNonAlloc(transform.position, radius, _colls, 1 << layerTarget) > 0;    
        return hitCount;
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
    public IEnumerator NotCollisionEntity(int layer)
    {
        gameObject.layer = _layIgnoreEntity; //layer que no detecta collision con alguna entidad
        yield return new WaitForSeconds(1f); // Ajusta el tiempo según sea necesario
        gameObject.layer = layer;

    }
}
