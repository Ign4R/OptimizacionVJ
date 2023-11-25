using System;
using UnityEngine;
using UnityEngine.WSA;

[RequireComponent(typeof(Rigidbody))]
public class BaseModel : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] private float _speed;

    public float Speed { get => _speed; private set => _speed = value; }

    public virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void MoveAndRotate(Vector3 dir)
    {
        dir.y = 0;
        Vector3 dirSpeed = dir * Speed;
        dirSpeed.y = _rb.velocity.y;
        _rb.velocity = dirSpeed;
        _rb.rotation = Quaternion.LookRotation(dir);
    }

    public virtual void GetDir(Vector3 dir)
    {
        transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    public void Shoot(GameObject bulletPrefab, Transform origin)
    {
        //TODO: Llamar al objectPool
        Instantiate(bulletPrefab, origin.position, origin.rotation);
    }

}
