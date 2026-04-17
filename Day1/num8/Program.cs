using System;

public class Num8
{
    static void Main()
    {
        int B = Random.Shared.Next(1, 101);
        int A = Random.Shared.Next(1, 101);
        Console.Write("A = ");
        Console.Write("B = ");


        double power = 1;
        for (int i = 1; i <= B; i++)
        {
            power *= A;
            Console.WriteLine($"{power:F2}");
        }
    }
}