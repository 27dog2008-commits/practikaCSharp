using System;
public class num3
{
    static void Main()
    {
        int a = Random.Shared.Next();
        int b = Random.Shared.Next();

        double z1 =( Math.Sin(a) + Math.Cos(2 * b - a)) / (Math.Cos(a) - Math.Sin(2 * b - a));

        double z2 = (1 + Math.Sin(2 * b)) / (Math.Cos(2 * b));

        Console.WriteLine("z1 =" + z1);
        Console.WriteLine("z2 =" + z2);
    }
}