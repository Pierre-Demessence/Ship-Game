using UnityEngine;

public class SpeedShipUpgrade : ShipUpgrade
{
    [SerializeField] private float _increase = 1;

    protected override string Name => "Speed";

    protected override void DoUpgrade(Ship ship)
    {
        ship.Speed += _increase;
    }
}