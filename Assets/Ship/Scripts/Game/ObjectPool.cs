using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    
    private readonly List<PoolableEntity> _pooledObjects = new List<PoolableEntity>();
    private readonly Dictionary<Type, List<PoolableEntity>> _pooledObjects2 = new Dictionary<Type, List<PoolableEntity>>(); // TODO try this ?
    
    [SerializeField] private bool _activated = true;
    [SerializeField] private List<ObjectPoolItem> _itemsToPool = new List<ObjectPoolItem>();
    public bool Activated => _activated;

    private void Awake()
    {
        MakeInstance();
    }

    private void MakeInstance()
    {
        if (Instance == null)
            Instance = this;
        else Debug.LogWarning("Trying to Instanciate another Object Pool");
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void OnEnable()
    {
        MakeInstance();
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
        /*foreach (var poolItem in _itemsToPool)
        {
            _pooledObjects2[poolItem.ObjectToPool.GetType()] = new List<PoolableEntity>();
            for (var i = 0; i < poolItem.AmountToPool; i++)
            {
                var obj = Instantiate(poolItem.ObjectToPool);
                obj.gameObject.SetActive(false);
                _pooledObjects2[poolItem.ObjectToPool.GetType()].Add(obj);
            }
        }*/
    }

    public T GetPooledObject<T>() where T : PoolableEntity => (T) GetPooledObject(typeof(T));

    public PoolableEntity GetPooledObject(Type T)
    {
        var poolItem = GetPoolItem(T);
        var pooledObject = Activated ? _pooledObjects.FirstOrDefault(o => !o.gameObject.activeInHierarchy && o.GetComponent(T) != null) : null;
        if (pooledObject == null)
        {
            pooledObject = Instantiate(poolItem.ObjectToPool);
            _pooledObjects.Add(pooledObject);
        }
        if (pooledObject)
        {
            pooledObject.ResetToDefault(poolItem.ObjectToPool);
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
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
}