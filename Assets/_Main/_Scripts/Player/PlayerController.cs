using UnityEngine;
public class PlayerController : Updateable, IDestroyable
{
    private PlayerModel _playerModel;
    private Vector3 _posSpawn;

    [SerializeField]
    private int layerEnemy;
    [SerializeField]
    private int layerBullet;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private Transform _origin;
  
    private void Awake()
    {
        _playerModel = GetComponent<PlayerModel>();
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
            _playerModel.MoveAndRotate(dir);
        }
        if(Input.GetMouseButtonDown(0))
        {
            _playerModel.Shoot(_bulletPrefab, _origin);
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
        if (collision.gameObject.layer == layerEnemy || collision.gameObject.layer == layerBullet) 
        {
            Die();
        }
    }

    public void Die()
    {
        _playerModel.Respawn(_posSpawn);
    }
}
