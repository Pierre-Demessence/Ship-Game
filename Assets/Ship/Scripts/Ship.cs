using UnityEngine;

public class Ship : Entity
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _speed = 10f;

    [SerializeField] private float _rof = 1f;

    private float _lastFire;
    
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
        var movX = Input.GetAxis("Horizontal") * _speed;// * Time.deltaTime;
        var movY = Input.GetAxis("Vertical") * _speed;// * Time.deltaTime;
        Rigidbody2D.velocity = new Vector2(movX, movY);

        if (Input.GetButton("Fire1") && Time.time - _lastFire > 1 / _rof)
        {
            var bullet = Instantiate(_bullet);
            bullet.transform.position = transform.position;
            _lastFire = Time.time;
        }
    }

    protected override void OnDie()
    {}

    private void OnCollisionEnter2D(Collision2D col)
    {
        Drop drop;
        if ((drop = col.gameObject.GetComponentInChildren<Drop>()) != null)
            drop.DoSomething(this);
            
    }
}