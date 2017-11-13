using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : Entity
{
    [SerializeField] private GameObject _orbitals;
    [SerializeField] private float _speed;
    [SerializeField] private Weapon _weapon;

    private float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private void FixedUpdate()
    {
        var movX = Input.GetAxis("Horizontal") * _speed;
        var movY = Input.GetAxis("Vertical") * _speed;
        Rigidbody2D.velocity = new Vector2(movX, movY);

        if (Input.GetButton("Fire"))
        {
            if (_weapon.Fire())
                FindObjectOfType<Game>().Score += 1;
            _orbitals.GetComponentsInChildren<Weapon>().ToList().ForEach(w => w.Fire());
        }
        if (Math.Abs(movX) > Mathf.Epsilon)
        {
            GetComponent<Animator>().SetBool("Move", true);
            GetComponent<SpriteRenderer>().flipX = movX > 0;
        }
        else
            GetComponent<Animator>().SetBool("Move", false);
    }

    protected override void OnDie()
    {
        SceneManager.LoadScene("MainMenu");
    }

    protected override void OnCollide(Collider2D col)
    {
        Drop drop;
        if ((drop = col.gameObject.GetComponent<Drop>()) != null)
        {
            Speed += 0.25f;
            _weapon.LevelUp();
            drop.Consume();
        }
    }
}