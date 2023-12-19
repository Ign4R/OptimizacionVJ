using System.Collections;
using UnityEngine;
/// <Stack>
/// Al utilizar un enfoque modular para cada enemigo, reducimos las llamadas a funciones
/// en el Stack. Por ej: las funciones de Movimiento y Disparo.
/// <Stack>
[RequireComponent(typeof(Rigidbody))]
public class BaseModel : Updateable  ///Especializacion derivando 
{    
    [SerializeField]
    private float _speed;
    [SerializeField]
    protected Transform _origin;
    [SerializeField]
    protected float _radius;

    protected int _layColls;

    private static Collider[] _colls = new Collider[3];

    public OnCollisionNonAloc OnCollisionNonAloc { get; protected set; }
    public Rigidbody Rb { get ; private set ; }
    public Collider[] Colls { get => _colls; set => _colls = value; }

    public virtual void Awake()
    {
        Rb = GetComponent<Rigidbody>();     
    }
    public override void Start()
    {
        base.Start();
        OnCollisionNonAloc = new OnCollisionNonAloc(_radius, _colls);
    }
    public void MoveAndRotate(Vector3 dir, Vector3 rotDir)
    {
        dir.y = 0;
        Vector3 dirSpeed = dir * _speed;
        dirSpeed.y = Rb.velocity.y;
        Rb.velocity = dirSpeed;
        Rb.rotation = Quaternion.LookRotation(rotDir);
    }
    public bool DetectCollision(int numContacts = 0)
    {
        return OnCollisionNonAloc.Sphere(transform.position, _layColls, numContacts);
    }

    ///<Especializacion>
    /// Especializacion se especializa un comportamiento generico mientras que 
    /// cada clase le da un tipo de bala especifica o layer para influir en el juego
    /// solamente usando una funcion generica sin la necesidad de crear dos tipos de bala
    /// <Especializacion>
    public void Shoot(ObjectPool<Updateable> objectPool, int layerTarget) 
    {
        Bullet bullet = (Bullet)objectPool.GetPooledObject();
        bullet.SetTarget(layerTarget);
        bullet.transform.SetPositionAndRotation(_origin.position, _origin.rotation);
    }

}
