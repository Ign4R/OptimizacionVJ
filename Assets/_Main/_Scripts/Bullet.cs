using UnityEngine;

public class Bullet : Updateable
{
    [SerializeField]
    private float _speed;

    public override void CustomUpdate()
    {
        Travel();
    }

    public void Travel()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}