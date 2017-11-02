using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private readonly List<PoolableEntity> _pooledObjects = new List<PoolableEntity>();
    [SerializeField] private List<ObjectPoolItem> _itemsToPool = new List<ObjectPoolItem>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Debug.LogWarning("Trying to Instanciate another Object Pool");
    }

    private void Start()
    {
        foreach (var poolItem in _itemsToPool)
            for (var i = 0; i < poolItem.AmountToPool; i++)
            {
                var obj = Instantiate(poolItem.ObjectToPool);
                obj.gameObject.SetActive(false);
                _pooledObjects.Add(obj);
            }
    }

    public GameObject GetPooledObject<T>() where T : PoolableEntity => GetPooledObject(typeof(T));

    public GameObject GetPooledObject(Type T)
    {
        var poolItem = GetPoolItem(T);
        var pooledObject = !poolItem.OnlyNew ? _pooledObjects.FirstOrDefault(o => !o.gameObject.activeInHierarchy && o.GetComponent(T) != null) : null;
        if (pooledObject == null && poolItem.ShouldExpand)
        {
            pooledObject = Instantiate(poolItem.ObjectToPool);
            _pooledObjects.Add(pooledObject);
        }
        if (pooledObject)
        {
            pooledObject.Reset(poolItem.ObjectToPool);
            pooledObject.gameObject.SetActive(true);
            return pooledObject.gameObject;
        }
        return null;
    }


    private ObjectPoolItem GetPoolItem<T>() where T : PoolableEntity => GetPoolItem(typeof(T));

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
    [SerializeField] public bool OnlyNew;
    [UsedImplicitly] public bool ShouldExpand;
}