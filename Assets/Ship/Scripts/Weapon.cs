using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PoolableEntity _bullet;
    [SerializeField] private float _rof;
    private float _lastFire;

    public float Rof
    {
        get { return _rof; }
        set { _rof = value; }
    }

    public bool Fire()
    {
        if (!(Time.time - _lastFire > 1 / Rof)) return false;

        var bullet = ObjectPool.Instance.GetPooledObject(_bullet.GetType());
        bullet.transform.position = transform.position;
        _lastFire = Time.time;
        return true;
    }
}