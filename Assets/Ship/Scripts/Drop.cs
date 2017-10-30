using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Drop : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D Rigidbody2D;

    private void Update()
    {
        Rigidbody2D.velocity = new Vector2(0, -0.5f);
    }

    public void DoSomething(Ship ship)
    {
        ship.Speed += 0.25f;
        ship.Rof += 0.75f;
        Destroy(gameObject);
    }
}