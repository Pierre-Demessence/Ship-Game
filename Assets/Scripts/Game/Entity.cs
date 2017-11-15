using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public Rigidbody2D Rigidbody2D { get; private set; }

    protected void Die(bool forceDie = false)
    {
        if (!forceDie) OnDie();
        Death();
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    protected virtual void OnCollide(Collider2D col)
    {
        if (col.gameObject.GetComponent<EntityDestroyer>() != null)
            Die(true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        OnCollide(col);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnCollide(col.collider);
    }

    protected abstract void OnDie();
}