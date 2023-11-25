using UnityEngine;
public class PlayerController : MonoBehaviour, IUpdateable
{
    PlayerModel _playerModel;
    public void Awake()
    {
        _playerModel = GetComponent<PlayerModel>();
        UpdateManager.Instance.Add(this);
    }

    public void CustomUpdate()
    {
        var v = Input.GetAxisRaw("Vertical");
        var h = Input.GetAxisRaw("Horizontal");


        var dir = new Vector3(h, 0, v);

        if (dir == Vector3.zero) return;
        _playerModel.Move(dir);
        _playerModel.LookDir(dir);
     
  
    }



}
