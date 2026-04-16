using System;
public class num7
{
    static void Main()
    {
        Console.WriteLine("Точка A");
        int x1 = Random.Shared.Next(11);
        int y1 = Random.Shared.Next(11);
        Console.WriteLine("x = " + x1);
        Console.WriteLine("y = " + y1);


        Console.WriteLine("Точка B");
        int x2 = Random.Shared.Next(11);
        int y2 = Random.Shared.Next(11);
        Console.WriteLine("x = " + x2);
        Console.WriteLine("y = " + y2);

        Console.WriteLine("Точка C");
        int x3 = Random.Shared.Next(11);
        int y3 = Random.Shared.Next(11);
        Console.WriteLine("x = " + x3);
        Console.WriteLine("y = " + y3);

        double a = Math.Sqrt(Math.Pow(x2 - x3, 2) + Math.Pow(y2 - y3, 2)); 
        double b = Math.Sqrt(Math.Pow(x1 - x3, 2) + Math.Pow(y1 - y3, 2));
        double c = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)); 

        double P = a + b + c;

        double p = P / 2; 
        double S = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

        Console.WriteLine();
        Console.WriteLine($"Длины сторон: a = {a:F2}, b = {b:F2}, c = {c:F2}");
        Console.WriteLine("P = " + P);
        Console.WriteLine("S = " + S);
    }
}