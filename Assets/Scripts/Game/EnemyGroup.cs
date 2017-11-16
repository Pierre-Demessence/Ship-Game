using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGroup", menuName = "Custom/EnemyGroup", order = 1)]
public class EnemyGroup : ScriptableObject
{
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    [SerializeField] private float _threat;
    public List<Enemy> Enemies => _enemies;
    public float Threat => _threat;
}