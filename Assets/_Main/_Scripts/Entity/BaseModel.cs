using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseModel : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    protected GameObject _bulletPrefab;
    [SerializeField]
    protected Transform _origin;
    protected int _bulletType;

    private Rigidbody _rb;
    public float Speed { get => _speed; private set => _speed = value; }
    public Rigidbody Rb { get ; private set ; }

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
        
    public virtual void GetDir(Vector3 dir)
    {
        transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    public void Shoot()
    {
        //TODO: Llamar al objectPool
        var bullet = Instantiate(_bulletPrefab, _origin.position, _origin.rotation);
        bullet.gameObject.layer = _bulletType;

    }

}
