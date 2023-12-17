using System.Collections;
using UnityEngine;

/// <Stack>
/// Al utilizar un enfoque modular para cada enemigo, reducimos las llamadas a funciones
/// en el Stack. Por ej: las funciones de Movimiento y Disparo.
/// <Stack>
[RequireComponent(typeof(Rigidbody))]
public class BaseModel : Updateable  ///Especializacion
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    protected Transform _origin;

    private int _layIgnoreEntity;
    protected int _bulletType;
    protected bool _destroyed;

    private static Collider[] _colls = new Collider[3];

    public float Speed { get => _speed; private set => _speed = value; }


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
    public bool CollisionNonAlloc(float radius, int layerMask, int count = 0)
    {
        return Physics.OverlapSphereNonAlloc(transform.position, radius, _colls, layerMask) > count;
    }
    public virtual void GetDir(Vector3 dir)
    {
        transform.rotation = Quaternion.LookRotation(dir.normalized);
    }
    public void Shoot(ObjectPool objectPool)
    {
        var bullet = objectPool.GetPooledObject();
        bullet.gameObject.layer = _bulletType;
        bullet.transform.SetPositionAndRotation(_origin.position, _origin.rotation);
    }
    public IEnumerator RespawnImmunity(Vector3 position, float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds); // Ajusta el tiempo según sea necesario
        transform.position = position;

    }
}
