using System;

public class Num1
{
    static void Main()
    {
        int distance = Random.Shared.Next(1000, 100000);
        Console.WriteLine("Расстояние в метрах: " + distance);
        int distanse_km = distance / 1000;
        Console.WriteLine("Расстояние в км: " + distanse_km);
    }
}