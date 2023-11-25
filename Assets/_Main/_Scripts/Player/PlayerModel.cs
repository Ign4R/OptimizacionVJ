using System;
using UnityEngine;

public class PlayerModel : BaseModel
{
    [SerializeField] 
    private float _mouseSensibilty = 100;


    public override void LookDir(Vector3 dir)
    {
        transform.rotation = Quaternion.LookRotation(dir.normalized);
    }
}
