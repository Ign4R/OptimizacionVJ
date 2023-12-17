using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerController : Updateable
{
    private PlayerModel _playerModel;
    private int _layColls;
    private int _layerBulletE;

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _radius;
    private bool _inhabilited;

    public GameObject Bullet { get => _bullet; }

    private void Awake()
    {    
        _playerModel = GetComponent<PlayerModel>();
        _layColls = 1 << LayerMask.NameToLayer("Enemy") | (1 << LayerMask.NameToLayer("BulletEnemy"));
        ///Caching :D

    }
    public override void Start()
    {
        base.Start();
        _playerModel.SetPosSpawn(transform.position);
      
    }
    public override void CustomUpdate()
    {
        if (!_inhabilited)
        {
            HandleDeathCollision();
        }
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
  
    public void HandleDeathCollision()
    {
        bool hasCollision = _playerModel.CollisionNonAlloc(_radius, _layColls);
        if (hasCollision && !_inhabilited)
        {
            Respawn();
            _inhabilited = true;
        }
    }
    public void Respawn()
    {
        _playerModel.Die();
    }
    private void OnEnable()
    {
        _inhabilited = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
