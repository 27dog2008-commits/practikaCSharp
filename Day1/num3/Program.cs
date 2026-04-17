using System;

public class Num3
{
    static void Main()
    {
        int price = Random.Shared.Next(1, 101);
        Console.WriteLine("Цента 1кг конфет: " + price);
        for (int i = 1; i < 11; i++)
        {
            Console.WriteLine($"Стоимость {i}кг конфет: {price * i}");
        }
    }
}