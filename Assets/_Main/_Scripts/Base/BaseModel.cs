using System;
using UnityEngine;
using UnityEngine.WSA;

[RequireComponent(typeof(Rigidbody))]
public class BaseModel : MonoBehaviour
{    
    Rigidbody _rb;
    [SerializeField] private float _speed;

    public float Speed { get => _speed; private set => _speed = value; }

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 dir)
    {
        dir.y = 0;
        Vector3 dirSpeed = dir * Speed;
        dirSpeed.y = _rb.velocity.y;
        _rb.velocity = dirSpeed;
    }

    public virtual void LookDir(Vector3 dir)
    {

    }
}
