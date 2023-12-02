using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

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
    private bool hasCollided;

    private void Awake()
    {
        _enemyModel = GetComponent<EnemyModel>();
        SetLayers();
        SetCooldown();
        _availables = _dirsAvailables.ToArray();
    }

    public override void CustomUpdate()
    {
        if (gameObject.activeSelf) 
        {
            TimerToShoot();
            _enemyModel.MoveAndRotate(transform.forward, _direction);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {    
        if (collision.gameObject.layer == _layerTarget)
        {
            Die();
        }

        if (!hasCollided)
        {
            if (collision.gameObject.layer == _layerWall && collision.gameObject.layer != gameObject.layer)
            {
                GetRandomDir();
            }

            else if (collision.gameObject.layer == gameObject.layer)
            {
                _direction = -_direction;
                hasCollided = true;
            }

        }


    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.layer == 10)
        {
            hasCollided = false;
        }

    }

    public void TimerToShoot()
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
            _direction = transform.forward;
        }

    }
    public void SetCooldown()
    {
        _currentCooldown = _cooldown;
    }
    public void SetLayers()
    {
        _layerTarget = LayerMask.NameToLayer("Player");
        _layerWall = LayerMask.NameToLayer("Wall");
    }
    private void OnEnable()
    {
        GameManager.Instance?.CheckCountInPool();
        _direction = transform.forward;
        SetCooldown();
        StartCoroutine(_enemyModel.NotCollisionEntity());

    }
    public void Die()
    {
        GameManager.Instance.EnemyPool.ReturnToPool(gameObject);
        GameManager.Instance.EnemyDie();
        print("Die Enemy");
    }



}
