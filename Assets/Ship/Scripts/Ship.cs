using UnityEngine;

public class Ship : Entity
{
    [SerializeField] private PoolableEntity _bullet;
    [SerializeField] private ObjectPool _bulletObjectPool;

    private float _lastFire;

    [SerializeField] private float _rof = 1f;
    [SerializeField] private float _speed = 10f;

    public int Score { get; set; }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public float Rof
    {
        get { return _rof; }
        set { _rof = value; }
    }

    private void Update()
    {
        var movX = Input.GetAxis("Horizontal") * _speed; // * Time.deltaTime;
        var movY = Input.GetAxis("Vertical") * _speed; // * Time.deltaTime;
        Rigidbody2D.velocity = new Vector2(movX, movY);

        if (Input.GetButton("Fire1") && Time.time - _lastFire > 1 / _rof)
        {
            var bullet = _bulletObjectPool.GetPooledObject(_bullet.GetType());
            bullet.transform.position = transform.position;
            _lastFire = Time.time;
            Score++;
        }
    }

    protected override void OnDie()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Drop drop;
        if ((drop = col.gameObject.GetComponentInChildren<Drop>()) != null)
            drop.DoSomething(this);
    }
}