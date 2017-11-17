using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class Ship : Entity
{
    [Range(0, 30)] [SerializeField] private float _invincibilityTime;
    private bool _invincible;
    [SerializeField] private int _lives;
    [SerializeField] private GameObject _orbitals;
    private int _selectedUpgrade = -1;
    [SerializeField] private float _speed;
    private int _upgradePoints;
    [SerializeField] private List<ShipUpgrade> _upgrades = new List<ShipUpgrade>();
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _orbital;

    private ShipUpgrade SelectedUpgrade => _selectedUpgrade > -1 ? _upgrades[_selectedUpgrade] : null;
    public string SelectedUpgradeName => SelectedUpgrade != null ? SelectedUpgrade.FullName : "None";
    public int SelectedUpgradeCost => SelectedUpgrade != null ? SelectedUpgrade.PointsCost : 0;

    public float Speed
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

    [UsedImplicitly]
    public int UpgradePoints
    {
        get { return _upgradePoints; }
        set
        {
            _upgradePoints = value;
            SetHighestUpgrade();
        }
    }

    public void AddOrbital()
    {
        var orbital = Instantiate(_orbital);
        orbital.transform.SetParent(_orbitals.transform);
    }

    public Weapon Weapon => _weapon;

    private void NextUpgrade()
    {
        var newIndex = _selectedUpgrade + 1;
        if (_upgrades.Count <= newIndex)
            SetLowestUpgrade();
        else
        {
            var newUpgrade = _upgrades[newIndex];
            if (!newUpgrade.IsMaxLevel && UpgradePoints >= newUpgrade.PointsCost)
                _selectedUpgrade = newIndex;
        }
    }

    private void PreviousUpgrade()
    {
        var newIndex = _selectedUpgrade - 1;
        if (newIndex < 0)
            SetHighestUpgrade();
        else
        {
            var newUpgrade = _upgrades[newIndex];
            if (!newUpgrade.IsMaxLevel && UpgradePoints >= newUpgrade.PointsCost)
                _selectedUpgrade = newIndex;
        }
    }

    private void Upgrade()
    {
        if (SelectedUpgrade == null) return;
        SelectedUpgrade.Upgrade(this);
        SetHighestUpgrade();
    }

    private void SetHighestUpgrade()
    {
        var newUpgrade = _upgrades.FindLast(upgrade => !upgrade.IsMaxLevel && UpgradePoints >= upgrade.PointsCost);
        _selectedUpgrade = newUpgrade != null ? _upgrades.IndexOf(newUpgrade): -1;
    }
    
    private void SetLowestUpgrade()
    {
        var newUpgrade = _upgrades.Find(upgrade => !upgrade.IsMaxLevel && UpgradePoints >= upgrade.PointsCost);
        _selectedUpgrade = newUpgrade != null ? _upgrades.IndexOf(newUpgrade): -1;
    }

    private void FixedUpdate()
    {
        var movX = Input.GetAxis("Horizontal") * Speed;
        var movY = Input.GetAxis("Vertical") * Speed;
        Rigidbody2D.velocity = new Vector2(movX, movY);

        if (Input.GetButton("Fire"))
        {
            Weapon.Fire();
            _orbitals.GetComponentsInChildren<Weapon>().ToList().ForEach(w => w.Fire());
        }
        if (Math.Abs(movX) > Mathf.Epsilon)
        {
            GetComponent<Animator>().SetBool("Move", true);
            GetComponent<SpriteRenderer>().flipX = movX > 0;
        }
        else
            GetComponent<Animator>().SetBool("Move", false);
        
        if (Input.GetButtonDown("PreviousUpgrade"))
            PreviousUpgrade();
        if (Input.GetButtonDown("NextUpgrade"))
            NextUpgrade();
        if (Input.GetButtonDown("Upgrade"))
        {
            Debug.Log("Upgrade");
            Upgrade();
        }
    }

    protected override void OnDie()
    {
        FindObjectOfType<Game>().GameOver();
    }

    protected override void OnCollide(Collider2D col)
    {
        Drop drop;
        if ((drop = col.gameObject.GetComponent<Drop>()) != null)
            drop.Consume(this);
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