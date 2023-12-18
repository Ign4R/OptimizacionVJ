using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerController : Updateable
{
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _radius;

    private PlayerModel _playerModel;
    private int _layColls;
    private int _layerBulletE;
    private bool _inhabilited;
    private Collider[] _colls = new Collider[4];

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
        if(!_inhabilited)
        {
            HandleDeathCollision();
        }
        else
        {
            Respawn();
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
    public void Respawn()
    {
        _playerModel.Die();
        _inhabilited = false;
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
        if (hasCollision)
        {
            _inhabilited = true;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
