using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    private readonly Random _random = new Random();
    private float _lastSpawn;

    [SerializeField] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;
    [SerializeField] private float _cooldown = 5;
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();

    private void Update()
    {
        if (Time.time - _lastSpawn > _cooldown)
        {
            var enemy = Instantiate(_enemies[_random.Next(_enemies.Count)]);
            enemy.transform.position = new Vector3(UnityEngine.Random.Range(_leftBound.position.x, _rightBound.position.x), transform.position.y, transform.position.z);
            _lastSpawn = Time.time;
        }
    }
}