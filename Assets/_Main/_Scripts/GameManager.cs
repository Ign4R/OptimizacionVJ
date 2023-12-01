using UnityEngine;

public class GameManager : Updateable
{
    [SerializeField] private int _bulletsToPool = 10;
    [Header("Prefabs")]
    [SerializeField] private GameObject _bulletGame;
    [SerializeField] private GameObject _enemy;
    public static GameManager Instance { get; private set; }
    public ObjectPool BulletPool { get ; private set ; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;


            FactoryObject bulletFactory = GetBulletFactory();
            BulletPool = GetBulletPool(bulletFactory);
          
        }
    }

    public FactoryObject GetBulletFactory()
    {
        GameObject parentBullets = new GameObject("Bullets");
        FactoryObject bulletFactory = new FactoryObject(_bulletGame, parentBullets.transform);

        return bulletFactory;
    }

    public ObjectPool GetBulletPool(FactoryObject factoryObject)
    {
        var bulletPool = new ObjectPool(factoryObject);
        bulletPool.Initialization(_bulletsToPool);
        return bulletPool;
    }
}