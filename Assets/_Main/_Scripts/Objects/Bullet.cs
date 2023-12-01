using UnityEngine;

public class Bullet : Updateable
{
    private int _layerWall;
    private Rigidbody _rb;
    private float _currentTime;
    [SerializeField] private float timeToDeactivate = 10;
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
    private void OnEnable()
    {
        _currentTime = 0f;
    }

    public void Travel()
    {
        if (_rb != null)
        {
            _rb.velocity = transform.forward * _speed;
        }

        // Verificar si ha pasado suficiente tiempo sin colisionar
        if (_currentTime >= timeToDeactivate)
        {         
            ReturnBullet();
        }
    }
    private void OnTriggerEnter(Collider other)
    { 
        
        if (other.gameObject.TryGetComponent<IDestroyable>(out var target))
        {
            ReturnBullet();
            target.Die();
          
        }
        if (other.gameObject.layer == _layerWall) 
        {
            ReturnBullet();
        }
    }

    public void ReturnBullet()
    {
        _currentTime = 0;
        GameManager.Instance.BulletPool.ReturnToPool(gameObject);
    }
  


}