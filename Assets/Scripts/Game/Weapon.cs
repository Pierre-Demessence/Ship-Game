using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _bulletSpacing = 1;
    private float _lastFire;
    private AudioSource _sound;
    [SerializeField] private List<WeaponLevel> _weaponLevels = new List<WeaponLevel>();
    private float Rof => CurrentWeaponLevel.Rof;
    private int BulletAmount => CurrentWeaponLevel.BulletAmount;
    private WeaponLevel CurrentWeaponLevel => _weaponLevels[Level - 1];
    private int Level { get; set; } = 1;
    private bool IsMaxLevel => Level == _weaponLevels.Count;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
    }

    public void LevelUp() => Level = !IsMaxLevel ? Level + 1 : Level;

    public bool Fire()
    {
        if (!(Time.time - _lastFire > 1 / Rof)) return false;

        var space = _bullet.GetComponent<SpriteRenderer>().bounds.size.x * _bulletSpacing;
        var left = transform.position.x - (_bullet.GetComponent<SpriteRenderer>().bounds.size.x + space) * ((BulletAmount - 1) / 2f);
        for (var i = 0; i < BulletAmount; ++i)
        {
            var x = left + (_bullet.GetComponent<SpriteRenderer>().bounds.size.x + space) * i;
            var bullet = (Bullet) ObjectPool.Instance.GetPooledObject(_bullet.GetType());
            bullet.transform.position = new Vector3(x, transform.position.y, transform.position.y);
            bullet.gameObject.layer = gameObject.layer;
            bullet.Rigidbody2D.velocity = transform.up * bullet.Speed;
        }
        _sound?.Play();
        _lastFire = Time.time;
        return true;
    }
}

[Serializable]
public class WeaponLevel
{
    [SerializeField] public int BulletAmount;
    [SerializeField] public float Rof;
}