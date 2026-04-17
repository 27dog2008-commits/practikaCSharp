using System;

class Num9
{
    static void Main()
    {
        Console.Write("A = "); 
        double A = double.Parse(Console.ReadLine());
        Console.Write("B = "); 
        double B = double.Parse(Console.ReadLine());
        Console.Write("M = "); 
        int M = int.Parse(Console.ReadLine());

        double H = (B - A) / M;
        double x = A;

        for (int i = 0; i <= M; i++)
        {
            double y = x * Math.Exp(-x) - 2; 
            Console.WriteLine($"x = {x:F4} | y = {y:F4}");
            x += H;
        }
    }
}