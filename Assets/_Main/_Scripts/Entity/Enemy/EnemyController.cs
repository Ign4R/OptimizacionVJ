using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class EnemyController : Updateable
{
    [SerializeField] private float _cooldown = 10;
    [SerializeField] private int maxStuckFrames = 15;
    [SerializeField] private float _radius = 4;
    [SerializeField] private LayerMask _layerColls;


    private EnemyModel _enemyModel;
    private Collider[] _colls = new Collider[5];


    private int stuckFrameCounter;
    private int _myLayer;
    private int _layerTarget;
    private float _currentCooldown;


    private Vector3 _lastPosition;
    private Vector3 _direction;
    private Vector3[] _availables = new Vector3[4];
    private HashSet<Vector3> _dirsAvailables = new HashSet<Vector3>
    {Vector3.forward,
    Vector3.back,
    Vector3.right,
    Vector3.left};

    private bool canMove = true;
    private bool isDestroyed = false;
    private bool canColl = true;

    private void Awake()
    {
        _myLayer = LayerMask.NameToLayer("Enemy");
        _layerTarget = LayerMask.NameToLayer("Player");
        _enemyModel = GetComponent<EnemyModel>();
        _availables = _dirsAvailables.ToArray();

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
        bool collisionDetected = _enemyModel.CollisionNonAlloc(_radius, _layerTarget);

        if (collisionDetected)
        {
            canMove = false;
        }
    }
    public void CheckStuckFrames()
    {
        // Checkea si la pos actual es igual a la anterior posicion
        if ((transform.position - _lastPosition).magnitude < 0.1f)
        {
            // incrementamos el contador
            stuckFrameCounter++;
            // si excede el limite cambiamos la dir
            if (stuckFrameCounter >= maxStuckFrames)
            {
                _direction = GetRandomDir();
                canMove = true;
                stuckFrameCounter = 0;
            }
        }
        else
        {
            // si la pos cambio, reseteamos el contador
            stuckFrameCounter = 0;
        }
        // actualiza la ultima posicion
        _lastPosition = transform.position;
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
        isDestroyed = false;
        GameManager.Instance?.CheckCountInPool();
        //StartCoroutine(_enemyModel.NotCollisionEntity(_myLayer));
        _direction = transform.forward;
        SetCooldown();

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}

