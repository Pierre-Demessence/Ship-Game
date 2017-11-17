using UnityEngine;

internal class RofShipUpgrade : ShipUpgrade
{
    [SerializeField] private float _increase = 1;
    
    protected override string Name => "Rof";

    protected override void DoUpgrade(Ship ship)
    {
        ship.Weapon.Rof += _increase;
    }
}