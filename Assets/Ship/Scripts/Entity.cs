using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected float _health = 1f;
    [SerializeField] protected float _power = 1f;
    [SerializeField] protected Rigidbody2D Rigidbody2D;

    private Entity _lastCollider;

    private float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (!(_health <= 0f)) return;
            OnDie();
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected abstract void OnDie();

    private void OnCollisionEnter2D(Collision2D col)
    {
        Entity entity;
        if ((entity = col.gameObject.GetComponent<Entity>()) != null)
            Health -= entity._power;
                
        if (col.gameObject.GetComponent<EntityDestroyer>() != null)
            Die();
    }
}