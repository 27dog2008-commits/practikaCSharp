public class BusFactory : VehicleFactory
{
    public override IVehicle CreateVehicle() => new Bus();
}
