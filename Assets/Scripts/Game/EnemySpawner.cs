using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    private readonly Random _random = new Random();
    [SerializeField] private float _cooldown = 5;
    [SerializeField] private bool _enabled = true;
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    private float _lastSpawn;
    [SerializeField] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;
    
    public bool Enabled
    {
        private get { return _enabled; }
        set { _enabled = value; }
    }

    private void Update()
    {
        if (!Enabled || !(Time.time - _lastSpawn > _cooldown)) return;
        var enemy = ObjectPool.Instance.GetPooledObject(_enemies[_random.Next(_enemies.Count)].GetType());
        enemy.transform.position = new Vector3(UnityEngine.Random.Range(_leftBound.position.x, _rightBound.position.x), transform.position.y, transform.position.z);
        _lastSpawn = Time.time;
    }
}