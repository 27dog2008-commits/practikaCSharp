using System;

class Program
{
    static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    static void Swap(ref double a, ref double b)
    {
        double temp = a;
        a = b;
        b = temp;
    }

    static void Main()
    {
        int x = 5, y = 10;
        Console.WriteLine($"До: x = {x}, y = {y}");
        Swap(ref x, ref y);
        Console.WriteLine($"После: x = {x}, y = {y}");

        double a = 2.5, b = 5.0;
        Console.WriteLine($"\nДо: a = {a}, b = {b}");
        Swap(ref a, ref b);
        Console.WriteLine($"После: a = {a}, b = {b}");
    }
}