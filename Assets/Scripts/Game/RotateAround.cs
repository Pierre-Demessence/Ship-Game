using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float speed;
    public Transform target;

    private readonly Vector3 zAxis = new Vector3(0, 0, 1);

    private void FixedUpdate()
    {
        transform.RotateAround(target.position, zAxis, speed);
    }
}