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


    protected int _bulletType;

    private static Collider[] _colls = new Collider[3];

    public float Speed { get => _speed; private set => _speed = value; }


    public Rigidbody Rb { get ; private set ; }
    public Collider[] Colls { get => _colls; set => _colls = value; }
    public bool Destroyed { get ; protected set ; }

    public virtual void Awake()
    {
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

    ///<Especializacion>
    /// Especializacion se especializa un comportamiento generico mientras que 
    /// cada clase le da un tipo de bala especifica o layer para influir en el juego
    /// solamente usando una funcion generica sin la necesidad de crear dos tipos de bala
    /// <Especializacion>
    public void Shoot(ObjectPool objectPool) 
    {
        var bullet = objectPool.GetPooledObject();
        bullet.gameObject.layer = _bulletType;
        bullet.transform.SetPositionAndRotation(_origin.position, _origin.rotation);
    }
    public IEnumerator RespawnImmunity(float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds); // Ajusta el tiempo según sea necesario
        gameObject.SetActive(false);


    }
}
