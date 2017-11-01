using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private readonly List<GameObject> _pooledObjects = new List<GameObject>();
    [SerializeField] private List<ObjectPoolItem> _itemsToPool = new List<ObjectPoolItem>();

    private void Start()
    {
        foreach (var poolItem in _itemsToPool)
            for (var i = 0; i < poolItem.AmountToPool; i++)
            {
                var obj = Instantiate(poolItem.ObjectToPool);
                obj.gameObject.SetActive(false);
                _pooledObjects.Add(obj.gameObject);
            }
    }

    public GameObject GetPooledObject<T>() where T : PoolableEntity
    {
        return GetPooledObject(typeof(T));
    }
    
    public GameObject GetPooledObject(Type T)
    {
        var pooledObject = _pooledObjects.FirstOrDefault(o => !o.activeInHierarchy && o.GetComponent(T) != null);
        var poolItem = GetPoolItem(T);
        if (pooledObject == null && poolItem.ShouldExpand)
        {
            pooledObject = Instantiate(poolItem.ObjectToPool).gameObject;
            _pooledObjects.Add(pooledObject);
        }
        pooledObject?.SetActive(true);
        return pooledObject;
    }

    private ObjectPoolItem GetPoolItem<T>() where T : PoolableEntity
    {
        return GetPoolItem(typeof(T));
    }
    
    private ObjectPoolItem GetPoolItem(Type T)
    {
        return _itemsToPool.FirstOrDefault(poolItem => poolItem.ObjectToPool.GetType() == T);
    }
}

[Serializable]
public class ObjectPoolItem
{
    [UsedImplicitly] public int AmountToPool;
    [UsedImplicitly] public PoolableEntity ObjectToPool;
    [UsedImplicitly] public bool ShouldExpand;
}