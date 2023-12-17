using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerController : Updateable
{
    private PlayerModel _playerModel;

    private Vector3 _posSpawn;
    private LayerMask _layerColl;
    private int _layerEnemy;
    private int _layerBulletE;

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _radius;


    public GameObject Bullet { get => _bullet; }

    private void Awake()
    {    
        _playerModel = GetComponent<PlayerModel>();
        _layerEnemy = LayerMask.NameToLayer("Enemy");
        _layerBulletE = LayerMask.NameToLayer("BulletEnemy");


    }
    public override void Start()
    {
        base.Start();
        _playerModel.SetPosSpawn(transform.position);
      
    }
    public override void CustomUpdate()
    {
        CheckCollisionDie();

        var dir = GetInputDir();
    
        if (dir != Vector3.zero)
        {
            _playerModel.MoveAndRotate(dir,dir);
        }
        if(Input.GetMouseButtonDown(0))
        {
            var objPool = GameManager.BulletPool;
            _playerModel.Shoot(objPool);
        }
    }
    public Vector3 GetInputDir()
    {
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        if (h != 0 && v != 0)
        {
            if (Mathf.Abs(h) > Mathf.Abs(v))
            {
                v = 0;
            }
            else
            {
                h = 0;
            }
        }    
        Vector3 inputDir = new Vector3(h, 0, v);
        return inputDir.normalized;
    }
  
    public void CheckCollisionDie()
    {
        bool hasCollision = _playerModel.CollisionNonAlloc(_radius,_layerEnemy);
        if (hasCollision)
        {
            _playerModel.Colls[0].GetComponent<IDestroyable>().Die();
            Respawn();
        }
    }
    public void Respawn()
    {
        _playerModel.Die();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
