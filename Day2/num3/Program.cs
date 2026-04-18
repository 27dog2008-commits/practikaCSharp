using System;

class Num3
{
    static void Main()
    {
        Console.Write("N = ");
        int N = int.Parse(Console.ReadLine());
        Console.Write("a = ");
        int a = int.Parse(Console.ReadLine());
        Console.Write("b = ");
        int b = int.Parse(Console.ReadLine());
        Console.Write("C = ");
        int C = int.Parse(Console.ReadLine());

        int[,] matrix = new int[N, N];
        Random rand = new Random();

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i, j] = rand.Next(a, b + 1);
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        long sumSquares = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (matrix[i, j] > C)
                {
                    sumSquares += matrix[i, j] * matrix[i, j];
                }
            }
        }


        Console.WriteLine($"\nСумма квадратов элементов > {C}: {sumSquares}");


        for (int i = 0; i < N; i++)
        {
            double rowSum = 0;
            for (int j = 0; j < N; j++)
                rowSum += matrix[i, j];
            Console.WriteLine($"Среднее строки {i}: {rowSum / N:F2}");
        }
    }
}