using UnityEngine;
public class GameManager : Updateable
{  
    [SerializeField] private int _maxGoals;
    [SerializeField] float _waitTimeToSpawn = 5f;
    [SerializeField] private int _bulletsInPool = 10;
    [SerializeField] private int _enemiesInPool = 100;

    [SerializeField] private Transform _posSpawnEnemies; 
    [SerializeField] private Transform _parentBullets;

    [Header("Prefabs")]
    [SerializeField] private Updateable _bullet;
    [SerializeField] private Updateable _enemy;

    private float _currTimeSpawn;

    public bool CanRunSpawn { get; private set; }
    public static ObjectPool<Updateable> BulletPool { get ; private set ; }
    public ObjectPool<Updateable> EnemyPool { get ; private set ; }
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
            _currTimeSpawn = _waitTimeToSpawn;

        }
    }
    public override void CustomUpdate()
    {
        TimerToSpawn();
    }
    public void InitializationPool()
    {
        FactoryObject<Updateable> bulletFactory = CreateFactory(_parentBullets, _bullet);
        FactoryObject<Updateable> enemyFactory = CreateFactory(_posSpawnEnemies, _enemy);
        BulletPool = CreatePool(bulletFactory, _bulletsInPool);
        EnemyPool = CreatePool(enemyFactory, _enemiesInPool);
    }

    public void TimerToSpawn()
    {      
        if (CanRunSpawn)
        {
            _currTimeSpawn += Time.deltaTime;
            if (_currTimeSpawn >= _waitTimeToSpawn)
            {
               
                _currTimeSpawn = 0;
                var enemy = EnemyPool.GetPooledObject();
                enemy.transform.position = _posSpawnEnemies.position;
            }
        }
    }
    public static void CounterEntity()
    {
        EntityCount++;
        if (EntityCount >= MaxGoals)
        {
            //TODO: Pop.Up GameOver Screen
        }
    }
    public void CheckIfPoolNotEmpty()
    {
        int countPool = EnemyPool.GetCountPool();  
        CanRunSpawn = countPool > 0;
    }
    public static FactoryObject<Updateable> CreateFactory(Transform parent, Updateable prefab)
    {
        FactoryObject<Updateable> bulletFactory = new FactoryObject<Updateable>(prefab, parent);
        return bulletFactory;
    }
    public static ObjectPool<Updateable> CreatePool(FactoryObject<Updateable> factoryObject, int countObj)
    {
        var pool = new ObjectPool<Updateable>(factoryObject);
        pool.Initialization(countObj);
        return pool;
    }
}

