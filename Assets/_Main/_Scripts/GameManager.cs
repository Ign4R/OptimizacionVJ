using UnityEngine;

public class GameManager : Updateable
{  
    private float _currentTime;
    [SerializeField] private int _maxGoals;
    [SerializeField] float _waitTimeToSpawn = 5f;
    [SerializeField] private int _bulletsInPool = 10;
    [SerializeField] private int _enemiesInPool = 100;

    [SerializeField] private Transform _positionSpawn;
    [SerializeField] private Transform _parentBullets;

    [Header("Prefabs")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _enemy;


    public bool CanRunSpawn { get; private set; }
    public static ObjectPool BulletPool { get ; private set ; }
    public  ObjectPool EnemyPool { get ; private set ; }
    public static GameManager Instance { get; private set; }
    public static int EntityCount { get ; private set ; }
    public static int MaxGoals { get ; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InitializationPool();
            Instance = this;
            MaxGoals = _maxGoals;
            CanRunSpawn = true;
            _currentTime = _waitTimeToSpawn;

        }
    }
    public override void CustomUpdate()
    {
        TimerToSpawn();
    }
    public void InitializationPool()
    {
        FactoryObject bulletFactory = CreateFactory(_parentBullets, _bullet);
        FactoryObject enemyFactory = CreateFactory(_positionSpawn, _enemy);
        BulletPool = CreatePool(bulletFactory, _bulletsInPool);
        EnemyPool = CreatePool(enemyFactory, _enemiesInPool);
    }
    public static FactoryObject CreateFactory(Transform parent, GameObject prefab)
    {
        FactoryObject bulletFactory = new FactoryObject(prefab, parent);
        return bulletFactory;
    }
    public static ObjectPool CreatePool(FactoryObject factoryObject, int countObj)
    {
        var pool = new ObjectPool(factoryObject);
        pool.Initialization(countObj);
        return pool;
    }
    public static void CounterEntity()
    {
        EntityCount++;
        if (EntityCount >= MaxGoals)
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


    public void CheckIfPoolNotEmpty()
    {
        int countPool = EnemyPool.GetCountPool();  
        CanRunSpawn = countPool > 0;
    }
}

