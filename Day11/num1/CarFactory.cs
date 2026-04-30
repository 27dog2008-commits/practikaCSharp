public class CarFactory : VehicleFactory
{
    public override IVehicle CreateVehicle() => new Car();
}
