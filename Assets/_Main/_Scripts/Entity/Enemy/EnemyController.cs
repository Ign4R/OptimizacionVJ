using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class EnemyController : Updateable
{
    [SerializeField] private float _cooldown = 10;
    [SerializeField] private float _maxStuckTime = 0.5f;
    [SerializeField] private float _radius = 4;
    [SerializeField] private LayerMask _layerColls;


    private EnemyModel _enemyModel;

    private float _stuckTime;
    private float _currentCooldown;


    private Vector3 _lastPosition;
    private Vector3 _direction;


    private bool canMove = true;

    private void Awake()
    {
        _enemyModel = GetComponent<EnemyModel>();

        SetCooldown();

    }

    public override void CustomUpdate()
    {
        CheckCollisionEntity();
        CheckStuckFrames();
        if (gameObject.activeSelf)
        {
            TimerToShoot();
            MoveEntity();    
        }
    }

    public void MoveEntity()
    {
        if (canMove)
        {
            _enemyModel.MoveAndRotate(transform.forward, _direction);
        }
    }

    public void CheckCollisionEntity()
    {      
        bool collisionDetected = _enemyModel.CollisionNonAlloc(_radius, gameObject.layer,1);

        if (collisionDetected)
        {
            canMove = false;
        }
    }
    public void CheckStuckFrames()
    {
        if ((transform.position - _lastPosition).magnitude < 0.1f)
        {
            // Incrementamos el contador de tiempo
            _stuckTime += Time.deltaTime;

            // Si excede el límite de tiempo, cambiamos la dirección
            if (_stuckTime >= _maxStuckTime)
            {
                _direction = GetRandomDir();
                canMove = true;
                _stuckTime = 0f;
            }
        }
        else
        {
            // Si la posición cambió, reseteamos el contador de tiempo
            _stuckTime = 0f;
        }

        // Actualiza la última posición
        _lastPosition = transform.position;
    }

    public void TimerToShoot()
    {
        _currentCooldown -= Time.deltaTime;
        if (_currentCooldown < 1)
        {
            var bulletPool = GameManager.BulletPool;
            _enemyModel.Shoot(bulletPool);
            _currentCooldown = _cooldown;
        }
    }

    public Vector3 GetRandomDir()
    {
        int randomInt = Random.Range(0, 4);
        switch (randomInt)
        {
            case 0:
                return Vector3.forward;
            case 1:
                return Vector3.right;
            case 2:
                return Vector3.back;
            case 3:
                return Vector3.left;
            default:
                return Vector3.zero;
        }

    }
    public void SetCooldown()
    {
        _currentCooldown = _cooldown;
    }

    private void OnEnable()
    {
        //StartCoroutine(_enemyModel.NotCollisionEntity(_myLayer));
        GameManager.Instance?.CheckIfPoolNotEmpty();
        _direction = transform.forward;
        SetCooldown();

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}

