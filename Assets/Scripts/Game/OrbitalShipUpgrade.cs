internal class OrbitalShipUpgrade : ShipUpgrade
{
    protected override string Name => "Orbital";

    protected override void DoUpgrade(Ship ship)
    {
        ship.AddOrbital();
    }
}