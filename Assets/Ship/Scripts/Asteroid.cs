using UnityEngine;

public class Asteroid : Enemy
{
    private void OnEnable()
    {
        Rigidbody2D.velocity = new Vector2(0, -1);
        Rigidbody2D.angularVelocity = Random.Range(-500, 500);
    }
}