using System;

public class Num5
{
    static void Main()
    {
        int M = Random.Shared.Next(1, 101);
        int N = Random.Shared.Next(1, M);

        Console.WriteLine("Число М: " + M);
        Console.WriteLine("Число N: " + N);

        if (M % N == 0)
        {
            Console.WriteLine("Частное от деления: " + M / N);
        }
        else
        {
            Console.WriteLine("M на N нацело не делится");
        }
    }
}