using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : PoolableEntity
{
    [SerializeField] private List<DropItem> _dropItems = new List<DropItem>();
    
    private readonly Random _random = new Random();

    protected override void OnDie()
    {
        var rng = (float) _random.NextDouble();
        foreach (var dropItem in _dropItems)
            if (rng * 100 <= dropItem.DropChance)
            {
                var drop = ObjectPool.Instance.GetPooledObject(dropItem.Drop.GetType());
                drop.transform.position = transform.position;
                break;
            }
    }
}

[Serializable]
public class DropItem
{
    [SerializeField] public Drop Drop;
    [SerializeField] [Range(0, 100)] public float DropChance;
}