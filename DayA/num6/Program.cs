using System;
public class num6
{
    static void Main()
    {
        int number = Random.Shared.Next(100,1000);
        Console.WriteLine("Число: " + number);

        int rezult = ((number % 100) * 10) + (number / 100);

        Console.WriteLine("Ответ: " + rezult);
    }
}