using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("тестирование паттерна наблюдатель\n");

        WeatherStation station = new WeatherStation();

        MobileApp myiPhone = new MobileApp();
        Website weatherPortal = new Website();

        station.RegisterObserver(myiPhone);
        station.RegisterObserver(weatherPortal);

        station.SetTemperature(18.0f);
        station.SetTemperature(21.5f);

        station.RemoveObserver(weatherPortal);

        station.SetTemperature(25.0f); 
    }
}