using UnityEngine;

public abstract class ShipUpgrade : MonoBehaviour
{
    private int _level;
    [SerializeField] private int _maxLevel;
    [SerializeField] private int _pointsCost;

    public bool IsMaxLevel => _level == _maxLevel;

    public string FullName => $"{Name} Lvl {_level+1} / {_maxLevel}";
    protected abstract string Name { get; }

    public int PointsCost => _pointsCost;

    protected abstract void DoUpgrade(Ship ship);

    public void Upgrade(Ship ship)
    {
        if (_level == _maxLevel || ship.UpgradePoints < _pointsCost) return;
        ++_level;
        ship.UpgradePoints -= _pointsCost;
        DoUpgrade(ship);
    }
}