using System;

public class Num7
{
    static void Main()
    {
        int A = Random.Shared.Next(1, 100);
        int B = Random.Shared.Next(A, 100);
        int X = Random.Shared.Next(0, 10);
        int Y = Random.Shared.Next(0, 10);

        Console.WriteLine($"A = {A}, B = {B}, X = {X}, Y = {Y}");

        for (int i = A; i <= B; i++)
        {
            if (i % 10 == X || i % 10 == Y)
                Console.WriteLine(i);
        }
    }
}