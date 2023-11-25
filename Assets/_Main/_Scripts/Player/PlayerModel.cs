using System;
using UnityEngine;

public class PlayerModel : BaseModel
{
    public void Respawn(Vector3 pos)
    {
        transform.position = pos;
    }
}
