using UnityEngine;

public class Drop : PoolableEntity
{
    private void Update()
    {
        Rigidbody2D.velocity = new Vector2(0, -0.5f);
    }

    protected override void OnDie()
    {
    }
}