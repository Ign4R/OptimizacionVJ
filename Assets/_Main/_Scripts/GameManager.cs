using UnityEngine;

public class GameManager : Updateable
{  
    private float _currentTime;
    private int _entityCount;
    [SerializeField] private int _maxEntities;
    [SerializeField] float _waitTimeToSpawn = 5f;
    [SerializeField] private int _bulletsInPool = 10;
    [SerializeField] private int _enemiesInPool = 100;

    [SerializeField] private Transform _positionSpawn;

    [Header("Prefabs")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _enemy;




    public bool CanRunSpawn { get; private set; }
    public ObjectPool BulletPool { get ; private set ; }
    public ObjectPool EnemyPool { get ; private set ; }
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            FactoryObject bulletFactory = GetFactory("Bullets",_bullet);
            FactoryObject enemyFactory = GetFactory("Enemies", _enemy);
            BulletPool = GetPool(bulletFactory, _bulletsInPool);
            EnemyPool = GetPool(enemyFactory, _enemiesInPool);         
            CanRunSpawn = true;
            _currentTime = _waitTimeToSpawn;
        }
    }
    public override void CustomUpdate()
    {
   
        TimerToSpawn();
    }
    public FactoryObject GetFactory(string nameParent, GameObject prefab)
    {
        GameObject parentBullets = new GameObject(nameParent);
        FactoryObject bulletFactory = new FactoryObject(prefab, parentBullets.transform);

        return bulletFactory;
    }

    public ObjectPool GetPool(FactoryObject factoryObject, int countObj)
    {
        var pool = new ObjectPool(factoryObject);
        pool.Initialization(countObj);
        return pool;
    }
    public void CounterEntity()
    {
        _entityCount++;
        if (_entityCount < _maxEntities)
        {
            CheckCountInPool();
        }
        else
        {
            //TODO: Pop.Up GameOver Screen
        }
           
    }


    public void TimerToSpawn()
    {      
        if (CanRunSpawn)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _waitTimeToSpawn)
            {
                _currentTime = 0;
                var enemy = EnemyPool.GetPooledObject();
                enemy.transform.position = _positionSpawn.position;
            }
        }
    }

    public void CheckCountInPool()
    {
        if (EnemyPool?.PooledObjects.Count >= 1) 
        {
            CanRunSpawn = true;
        }
        else 
        {
            CanRunSpawn = false;
        }
      
    }
}

