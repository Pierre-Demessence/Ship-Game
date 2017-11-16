using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    private readonly Random _random = new Random();
    [SerializeField] private float _baseCooldown;
    private float _cooldown;
    [SerializeField] private bool _enabled = true;
    [SerializeField] private List<EnemyGroup> _enemyGroups = new List<EnemyGroup>();
    private float _lastSpawn;
    [SerializeField] private Transform _leftBound;
    [SerializeField] private int _pointsPerDifficulty;
    [SerializeField] private Transform _rightBound;

    public bool Enabled
    {
        private get { return _enabled; }
        set { _enabled = value; }
    }

    [UsedImplicitly]
    public float Difficulty => FindObjectOfType<Game>().Score / (float) _pointsPerDifficulty + 1f;

    private void Start()
    {
        _cooldown = _baseCooldown;
    }

    private double CalculateWeight(EnemyGroup enemyGroup) => 1 / (Mathf.Abs(Difficulty - enemyGroup.Threat) + 1);

    private float CalculateCooldown(EnemyGroup enemyGroup) => Mathf.Clamp(_baseCooldown * enemyGroup.Threat / Difficulty, _baseCooldown * 0.25f, _baseCooldown * 4f) * ((enemyGroup.Enemies.Count - 1f) / 5f + 1f);

    private void Update()
    {
        if (!Enabled || _enemyGroups.Count == 0 || !(Time.time - _lastSpawn > _cooldown)) return;

        var weightedGroups = new Dictionary<EnemyGroup, double>();

        // Make Weighted Table of EnemyGroups
        foreach (var enemyGroup in _enemyGroups)
            weightedGroups[enemyGroup] = CalculateWeight(enemyGroup);

        // Generate a random number between 0 and the sum of all weights.
        var d = _random.NextDouble() * weightedGroups.Sum(pair => pair.Value);

        // Select the random EnemyGroup based on the random and the current weight sum
        double currentWeightSum = 0;
        EnemyGroup selectedEnemyGroup = null;
        foreach (var enemyGroup in weightedGroups)
        {
            currentWeightSum += enemyGroup.Value;
            if (d <= currentWeightSum)
            {
                selectedEnemyGroup = enemyGroup.Key;
                break;
            }
        }

        // Spawn each enemies of the EnemyGroup
        if (selectedEnemyGroup == null) return;
        foreach (var enemy in selectedEnemyGroup.Enemies)
        {
            var spawnedEnemy = ObjectPool.Instance.GetPooledObject(enemy.GetType());
            spawnedEnemy.transform.position = new Vector3(UnityEngine.Random.Range(_leftBound.position.x, _rightBound.position.x), transform.position.y, transform.position.z);
        }

        _cooldown = CalculateCooldown(selectedEnemyGroup);
        _lastSpawn = Time.time;
    }
}