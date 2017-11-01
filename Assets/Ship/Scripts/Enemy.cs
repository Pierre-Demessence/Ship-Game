using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : PoolableEntity
{
    private readonly Random _random = new Random();
    [SerializeField] private Drop _drop;
    [SerializeField] [Range(0, 100)] private float _dropChance;

    protected override void OnDie()
    {
        if ((float) _random.NextDouble() * 100 <= _dropChance)
        {
            var drop = Instantiate(_drop);
            drop.transform.position = transform.position;
        }
    }
}