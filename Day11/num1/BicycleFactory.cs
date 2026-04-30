public class BicycleFactory : VehicleFactory
{
    public override IVehicle CreateVehicle() => new Bicycle();
}
