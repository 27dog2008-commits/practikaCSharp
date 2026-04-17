using System;

public class Num2
{
    static void Main()
    {
        Console.WriteLine("Данное целое число являётся чётным двузначным числом?");
        int number = Random.Shared.Next(1,200);
        Console.WriteLine("Число: " + number);

        if (number > 9 && number < 100)
        {
            Console.WriteLine("Данное число двузначное");
        }
        else
        {
            Console.WriteLine("Данное число не двузначное");
        }

        if (number % 2 == 0) {
            Console.WriteLine("Данное число чётное");
        }
        else
        {
            Console.WriteLine("Данное число нечётное");
        }

    }
}