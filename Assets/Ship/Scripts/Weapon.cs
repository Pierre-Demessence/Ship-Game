using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PoolableEntity _bullet;
    [SerializeField] private int _bulletAmount;
    [SerializeField] private float _bulletSpacing = 1;
    private float _lastFire;
    [SerializeField] private float _rof;

    public float Rof
    {
        get { return _rof; }
        set { _rof = value; }
    }

    public bool Fire()
    {
        if (!(Time.time - _lastFire > 1 / Rof)) return false;

        var space = _bullet.GetComponent<SpriteRenderer>().bounds.size.x * _bulletSpacing;
        var left = transform.position.x - (_bullet.GetComponent<SpriteRenderer>().bounds.size.x + space) * ((_bulletAmount - 1) / 2f);
        for (var i = 0; i < _bulletAmount; ++i)
        {
            var x = left + (_bullet.GetComponent<SpriteRenderer>().bounds.size.x + space) * i;
            var bullet = ObjectPool.Instance.GetPooledObject(_bullet.GetType());
            bullet.transform.position = new Vector3(x, transform.position.y, transform.position.y);
        }
        Debug.Log(_bullet.GetComponent<SpriteRenderer>().bounds.size);
        _lastFire = Time.time;
        return true;
    }
}