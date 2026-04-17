using System;

class Num10
{
    static void Main()
    {
        for (int i = 10; i <= 99; i++)
        {
            int tens = i / 10;
            int units = i % 10;
            int sumSquares = tens * tens + units * units;

            if (sumSquares % 13 == 0)
                Console.WriteLine(i);
        }
    }
}