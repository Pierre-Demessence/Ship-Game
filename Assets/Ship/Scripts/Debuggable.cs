using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public abstract class Debuggable : MonoBehaviour
{
    [SerializeField] protected TextMesh TextMesh;
    protected abstract void Debug();

    private void LateUpdate()
    {
        Debug();
    }
}