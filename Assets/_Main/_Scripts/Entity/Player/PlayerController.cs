using UnityEngine;
public class PlayerController : Updateable, IDestroyable
{
    private PlayerModel _playerModel;
    private Vector3 _posSpawn;
    private int _layerEnemy;
    private int _layerBulletE;

    [SerializeField]
    private GameObject _bullet;

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
        _posSpawn = transform.position;
    }
    public override void CustomUpdate()
    {
       
        var dir = GetInputDir();

        if (dir != Vector3.zero)
        {
            _playerModel.MoveAndRotate(dir,dir);
        }
        if(Input.GetMouseButtonDown(0))
        {
            var objPool = GameManager.Instance.BulletPool;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _layerEnemy || collision.gameObject.layer == _layerBulletE) 
        {
            Die();
        }
    }
    public void Die()
    {
        _playerModel.Respawn(_posSpawn);
    }
}
