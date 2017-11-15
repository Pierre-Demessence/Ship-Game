using System;
using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : Entity
{
    [Range(0, 30)] [SerializeField] private float _invincibilityTime;
    private bool _invincible;
    [SerializeField] private int _lives;
    [SerializeField] private GameObject _orbitals;
    [SerializeField] private float _speed;
    [SerializeField] private Weapon _weapon;

    private float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [UsedImplicitly]
    public int Lives
    {
        get { return _lives; }
        private set
        {
            _lives = value;
            if (!(_lives <= 0)) return;
            Die();
        }
    }

    private void FixedUpdate()
    {
        var movX = Input.GetAxis("Horizontal") * _speed;
        var movY = Input.GetAxis("Vertical") * _speed;
        Rigidbody2D.velocity = new Vector2(movX, movY);

        if (Input.GetButton("Fire"))
        {
            _weapon.Fire();
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
        FindObjectOfType<Game>().GameOver();
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
        if (col.gameObject.GetComponent<Enemy>() != null)
            Hit();
    }

    private void Hit()
    {
        if (_invincible) return;
        Lives--;
        _invincible = true;
        GetComponent<Animator>().SetBool("Blink", true);
        StartCoroutine(Foo());
    }

    private IEnumerator Foo()
    {
        yield return new WaitForSecondsRealtime(_invincibilityTime);
        GetComponent<Animator>().SetBool("Blink", false);
        _invincible = false;
    }
}