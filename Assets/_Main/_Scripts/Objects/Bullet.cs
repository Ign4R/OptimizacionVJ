using UnityEngine;

public class Bullet : Updateable
{
    private int _layerWall;
    private Rigidbody _rb;
    private float _currentTime=0;
    [SerializeField] private float _timeToDeactivate = 10;
    [SerializeField] private float _speed = 40;

    private void Awake()
    {
        _layerWall = LayerMask.NameToLayer("Wall");
        _rb = GetComponent<Rigidbody>();
    }

    public override void CustomUpdate()
    {
        Travel();
    }

    public void Travel()
    {
        //if (gameObject.activeSelf)
        //{
        //    Timer();
        //}
        if (_rb != null)
        {
            _rb.velocity = transform.forward * _speed;
        }
        // Verificar si ha pasado suficiente tiempo sin colisionar
        //if (_currentTime >= _timeToDeactivate)
        //{
        //    ReturnBullet();
        //}
    }

    public void Timer()
    {
        _currentTime += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    { 
        
        if (other.gameObject.TryGetComponent<IDestroyable>(out var target))
        {
            ReturnBullet();
            //target.Die();
          
        }
        if (other.gameObject.layer == _layerWall) 
        {
            ReturnBullet();
        }
    }

    public void ReturnBullet()
    {
        //_currentTime = _timeToDeactivate;
        gameObject.layer = default;
        transform.position = transform.parent.position;
        GameManager.BulletPool.ReturnToPool(gameObject);
    }
  


}