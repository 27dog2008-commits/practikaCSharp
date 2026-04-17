using System;

public class Num4
{
    static void Main()
    {
        int x = Random.Shared.Next(100);

        double y;

        if (x >= 1 && x <= 3)
        {
            y = 2 * x * x - 3 * Math.Exp(Math.Sin(x));
        }
        else if (x > 3)
        {
            y = 3 * Math.Tan(x);
        }
        else
        {
            y = double.NaN; 
            Console.WriteLine("x < 1 — функция не определена");
        }

        if (x >= 1)
            Console.WriteLine($"y = {y:F2}");
    }
}