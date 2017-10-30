using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float _health = 1f;
    [SerializeField] private float _power = 1f;
    [SerializeField] protected Rigidbody2D Rigidbody2D;

    private float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0f) Die();
        }
    }

    private void Die()
    {
        OnDie();
        Destroy(gameObject);
    }

    protected abstract void OnDie();

    private void OnCollisionEnter2D(Collision2D col)
    {
        Entity entity;
        if ((entity = col.gameObject.GetComponentInChildren<Entity>()) != null)
            Health -= entity._power;
    }
}