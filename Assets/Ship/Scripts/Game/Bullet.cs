using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : PoolableEntity
{
    [SerializeField] private float _power = 1f;
    [SerializeField] private float _speed = 5f;
    
    public float Power => _power;
    public float Speed => _speed;

    protected override void OnDie()
    {
    }

    public override void ResetToDefault(PoolableEntity origin)
    {
    }

    protected override void OnCollide(Collider2D col)
    {
        base.OnCollide(col);
        if ((col.gameObject.GetComponent<Enemy>()) != null)
            Die();
    }
}