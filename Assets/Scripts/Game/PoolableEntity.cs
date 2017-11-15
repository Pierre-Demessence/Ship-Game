public abstract class PoolableEntity : Entity
{
    protected override void Death()
    {
        if (ObjectPool.Instance.Activated) gameObject.SetActive(false);
        else base.Death();
    }

    public abstract void ResetToDefault(PoolableEntity origin);
}