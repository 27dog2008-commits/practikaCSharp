using System;
// это задание такое же как и 8 
public class num5
{
    static void Main()
    {
        double x = 4.3;
        double y = (1 + Math.Sqrt(Math.Abs(3 - x * x))) / Math.Atan(x * x) - Math.Exp(Math.Sin(Math.Sqrt(x)));
        Console.WriteLine("y = " + y);
    }
}