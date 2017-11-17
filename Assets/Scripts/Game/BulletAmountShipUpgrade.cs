internal class BulletAmountShipUpgrade : ShipUpgrade
{
    protected override string Name => "BulletAmount";

    protected override void DoUpgrade(Ship ship)
    {
        ++ship.Weapon.BulletAmount;
    }
}