using UnityEngine;
public class Bullet : Updateable
{  
    [SerializeField] 
    private float _speed = 40;
    [SerializeField]
    private float _radius;

    private Collider[] _colls = new Collider[1];
    private Rigidbody _rb;
    private OnCollisionNonAloc _onCollision;

    private int _layTarget;
    private int _layerWall;
    private bool _hit;
    //private float _currentTime=0;
    //[SerializeField] 
    //private float _timeToDeactivate = 10;

    public override void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public override void Start()
    {
        base.Start();
        _layerWall = LayerMask.NameToLayer("Wall");
        _onCollision = new OnCollisionNonAloc(_radius, _colls);
    }
    public void SetTarget(int layerTarget)
    {
        _layTarget = layerTarget;
    }
    public override void CustomUpdate()
    {
        Travel();
        DetectCollision();        
    }
    public void DetectCollision()
    {      
        bool hasCollision = _onCollision.Sphere(transform.position);
        if (hasCollision && !_hit)
        {
            Collider coll = _colls[0];

            if (coll.gameObject.layer == _layTarget)
            {
                if (coll.TryGetComponent<IDestroyable>(out var target))
                {
                    ReturnBullet();
                    target.Die(true);
                    _hit = true;
                }
            }
            else if (coll.gameObject.layer == _layerWall)
            {
                ReturnBullet();
                _hit = true;
            }
        }
    }
    public void Travel()
    {
        if (_rb != null)
        {
            _rb.velocity = transform.forward * _speed;
        }

        //if (gameObject.activeSelf)
        //{
        //    Timer();
        //}
        //Verificar si ha pasado suficiente tiempo sin colisionar
        //if (_currentTime >= _timeToDeactivate)
        //{
        //    ReturnBullet();
        //}
    }

    //public void Timer()
    //{
    //    _currentTime += Time.deltaTime;
    //}

    public void ReturnBullet()
    {
        //_currentTime = _timeToDeactivate;
        _layTarget = default;
        transform.position = transform.parent.position;
        GameManager.BulletPool.ReturnToPool(this);
    }

    private void OnEnable()
    {
        _hit = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}