using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropTable", menuName = "Custom/DropTable", order = 1)]
public class DropTable : ScriptableObject
{
    [SerializeField] private List<DropItem> _dropItems = new List<DropItem>();
    public List<DropItem> DropItems => _dropItems;
}


[Serializable]
public class DropItem
{
    [SerializeField] public Drop Drop;
    [SerializeField] [Range(0, 100)] public float DropChance;
}