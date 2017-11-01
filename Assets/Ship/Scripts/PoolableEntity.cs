public abstract class PoolableEntity : Entity
{
    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}