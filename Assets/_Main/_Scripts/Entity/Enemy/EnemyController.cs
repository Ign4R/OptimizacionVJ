using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : Updateable, IDestroyable
{
    [SerializeField] private float _cooldown = 10;


    private EnemyModel _enemyModel;
    private int _layerTarget;
    private int _layerWall;
    private float _currentCooldown;
    private Vector3 _direction;


    private Vector3[] _availables = new Vector3[3];
    private HashSet<Vector3> _dirsAvailables = new HashSet<Vector3> 
    {Vector3.forward,
    Vector3.back,
    Vector3.right,
    Vector3.left};

    

    public override void Start()
    {
        base.Start();
        _enemyModel = GetComponent<EnemyModel>();
        _layerTarget = LayerMask.NameToLayer("Player");
        _layerWall = LayerMask.NameToLayer("Wall");
        _currentCooldown = _cooldown;
        _availables = _dirsAvailables.ToArray();
        GetRandomDir();
    }
    public override void CustomUpdate()
    {

        Timer();
        _enemyModel.MoveAndRotate(transform.forward, _direction);
    }
    private void OnCollisionEnter(Collision collision)
    {    
        if (collision.gameObject.layer == _layerTarget)
        {
            Die();
        }
        if (collision.gameObject.layer == _layerWall)
        { 
            GetRandomDir();

        }

        if (collision.gameObject.layer == gameObject.layer)
        {
            _direction = -_direction;
        }

    }

    public void Die()
    {
        ///TODO: HACER POOL
        print("Die Enemy");
    }

    public void Timer()
    {
        _currentCooldown -= Time.deltaTime;
        if (_currentCooldown < 1)
        {
            var bulletPool = GameManager.Instance.BulletPool;
            _enemyModel.Shoot(bulletPool);
            _currentCooldown = _cooldown;
        }
    }

    public void GetRandomDir()
    {

      
        if (_direction != Vector3.zero)
        {

            var dirRemove = _direction;
            _dirsAvailables.Remove(_direction);
            _availables = _dirsAvailables.ToArray();
            _dirsAvailables.Add(dirRemove);
            _direction = _availables[Random.Range(0, _availables.Length)];
        }
        else
        {
            print("IGUAL A VECTOR ZERO");
            _direction = _availables[Random.Range(0, _availables.Length)];
        }


    }
}
