using System.Collections.Generic;
using UnityEngine;

public class Ship : Entity
{
    [SerializeField] private float _speed;
    [SerializeField] private List<Weapon> _weapons = new List<Weapon>();

    public int Score { get; set; }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private void Update()
    {
        var movX = Input.GetAxis("Horizontal") * _speed; // * Time.deltaTime;
        var movY = Input.GetAxis("Vertical") * _speed; // * Time.deltaTime;
        Rigidbody2D.velocity = new Vector2(movX, movY);

        if (Input.GetButton("Fire1"))
            _weapons.ForEach(w => w.Fire());
    }

    protected override void OnDie()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Drop drop;
        if ((drop = col.gameObject.GetComponent<Drop>()) != null)
        {
            Speed += 0.25f;
            Score += 50;
            _weapons.ForEach(w => w.Rof += 0.75f);
        }
    }
}