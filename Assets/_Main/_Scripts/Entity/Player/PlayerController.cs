using UnityEngine;
public class PlayerController : Updateable
{
    private PlayerModel _playerModel;
    private int _layTarget;
    private bool _inhabilited;

    private void Awake()
    {      
        _playerModel = GetComponent<PlayerModel>();
        _layTarget = LayerMask.NameToLayer("Enemy");
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

        if (dir.magnitude != 0) 
        {
            _playerModel.MoveAndRotate(dir,dir);
        }
        if(Input.GetMouseButtonDown(0))
        {
            var objPool = GameManager.BulletPool;
            _playerModel.Shoot(objPool,_layTarget);
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
        bool hasCollision = _playerModel.DetectCollision();
        if (hasCollision)
        {
            _inhabilited = true;
        }
    }

}
