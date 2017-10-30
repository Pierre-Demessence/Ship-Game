using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : Entity
{
    [SerializeField] private float _speed = 5f;

    private void Update()
    {
        Rigidbody2D.velocity = new Vector2(0, _speed);
    }

    protected override void OnDie()
    {}
}