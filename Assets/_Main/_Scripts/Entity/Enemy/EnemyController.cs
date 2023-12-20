using UnityEngine;
public class EnemyController : Updateable
{
    [SerializeField] private float _cooldownShoot = 4;
    [SerializeField] private float _maxStuckTime = 0.5f;
   
    private EnemyModel _enemyModel;
    private bool _enabledParent;
    private float _stuckTime;
    private float _currTimeShoot;
    private int _layerTarget;
    private bool _targetCached;
    private bool canMove = true;
    private bool _inhabilited;

    private Vector3 _lastPosition;
    private Vector3 _direction;

    public override void Awake()
    {
        _enemyModel = GetComponent<EnemyModel>();
       _enabledParent = transform.parent == null || transform.parent.gameObject.activeInHierarchy;
    }
    public override void Start()
    {
        base.Start();     
        _layerTarget = LayerMask.NameToLayer("Player");
        SetCooldownShoot();
       
    }
    public override void CustomUpdate()
    {
        if (!_inhabilited)
        {
            CheckCollisionEntity();
            HandleDeathCollision();
        }
        else
        {
            Recycle();
        }


        if (_enabledParent && gameObject.activeInHierarchy)  //TODO
        {        
            CheckStuckFrames();
            TimerToShoot();
            MoveEntity();
        }
    }

    public void Recycle()
    {
        _enemyModel.Die();
        _inhabilited = false;
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
        bool hasCollision = _enemyModel.DetectCollision(1);
        if (hasCollision && canMove) 
        {                 
            _targetCached = _enemyModel.Colls[0].gameObject.layer == _layerTarget;  ///LAZY COMPUTATION ///CACHING
            canMove = false;
        }

    }
    /// <LazyComputation>
    /// Aplicamos el lazy computation al retrasar o postergar el calculo costoso del bucle foreach
    /// <LazyComputation>
    public void HandleDeathCollision()
    {
        if (_targetCached) ///LAZY COMPUTATION
        {       
            foreach (var item in _enemyModel.Colls)
            {
                if (item != null)
                {
                    if (item.gameObject.layer == _layerTarget)
                    {
                        _targetCached = false;
                        _inhabilited = true;
                    }
                }
            }
        }
 
    }
    public void CheckStuckFrames()
    {
        if ((transform.position - _lastPosition).magnitude <= 0.2f)
        {          
            _stuckTime += Time.deltaTime; // Incrementamos el contador de tiempo
            canMove = true;          
            if (_stuckTime >= _maxStuckTime)  // Si excede el límite de tiempo, cambiamos la dirección
            {            
                _direction = _enemyModel.GetRandomDir();
                _stuckTime = 0f;
            }
        }
        else
        {          
            _stuckTime = 0f; // Si la posición cambió, reseteamos el contador de tiempo
        }     
        _lastPosition = transform.position;  // Actualiza la última posición
    }

    public void TimerToShoot()
    {
        _currTimeShoot -= Time.deltaTime;
        if (_currTimeShoot < 1)
        {
            var bulletPool = GameManager.BulletPool;
            _enemyModel.Shoot(bulletPool,_layerTarget);
            _currTimeShoot = _cooldownShoot;
        }
    }

    public void SetCooldownShoot()
    {
        _currTimeShoot = _cooldownShoot;
    }

    private void OnEnable()
    {
        GameManager.Instance?.CheckIfPoolNotEmpty();
        _direction = transform.forward;
        SetCooldownShoot();

    }
 
}

