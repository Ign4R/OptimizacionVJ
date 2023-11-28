using UnityEngine;

public class Bullet : Updateable
{
    [SerializeField]
    private float _speed;
    private int _layerWall;
    private Rigidbody _rb;
    public override void Start()
    {
        base.Start();
        _layerWall = LayerMask.NameToLayer("Wall");
        _rb = GetComponent<Rigidbody>();
    }
    public override void CustomUpdate()
    {
        Travel();
    }
    public void Travel()
    {
        if (_rb != null)
        {
            _rb.velocity = transform.forward * _speed;
        }
    }
    private void OnTriggerEnter(Collider other)
    { 
        
        if (other.gameObject.TryGetComponent<IDestroyable>(out var target))
        {
            //TODO: Llamar al pool
            target.Die();
            Destroy(gameObject); //SACAR
        }
        if (other.gameObject.layer == _layerWall) 
        {
            //TODO: Llamar al pool
            Destroy(gameObject); //SACAR
        }
    }

  


}