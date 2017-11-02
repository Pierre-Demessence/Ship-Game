public abstract class PoolableEntity : Entity
{
    protected override void Die()
    {
        gameObject.SetActive(false);
    }

    public virtual void Reset(PoolableEntity origin)
    {
        _health = origin._health;
        _power = origin._power;
    }
}