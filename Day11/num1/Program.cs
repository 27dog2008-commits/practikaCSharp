using System;

class Num1
{
    static void Main(string[] args)
    {
        Console.WriteLine("Паттерн Фабричный метод");

        VehicleFactory factory;

        factory = new CarFactory();
        IVehicle car = factory.CreateVehicle();
        car.Move();

        factory = new BusFactory();
        IVehicle bus = factory.CreateVehicle();
        bus.Move();

        factory = new BicycleFactory();
        IVehicle bike = factory.CreateVehicle();
        bike.Move();
    }
}