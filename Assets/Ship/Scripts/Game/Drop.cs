using UnityEngine;

public class Drop : PoolableEntity
{
    private void Update()
    {
        Rigidbody2D.velocity = new Vector2(0, -0.5f);
    }

    public void Consume()
    {
        Die();
    }

    protected override void OnDie()
    {
    }

    public override void ResetToDefault(PoolableEntity origin)
    {
    }
}