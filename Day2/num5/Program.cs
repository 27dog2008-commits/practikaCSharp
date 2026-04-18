using System;
using System.Collections.Generic;

class Num5
{
    static void Main()
    {
        int[][] jagged = new int[][]
        {
            new int[] { 1, 2, -3 },
            new int[] { 0, 0, 0 },
            new int[] { 5, -5, 0 },
            new int[] { 1, 1, 1 }
        };

        Console.WriteLine("Исходный массив:");
        foreach (var row in jagged)
        {
            Console.WriteLine(string.Join(" ", row));
        }

        List<int[]> result = new List<int[]>();
        foreach (var row in jagged)
        {
            int sum = 0;
            foreach (int val in row)
            {
                sum += val;
                if (sum != 0)
                {
                    result.Add(row);
                }
            }
        }

        Console.WriteLine("\nПосле удаления строк с нулевой суммой:");
        foreach (var row in result)
        {
            Console.WriteLine(string.Join(" ", row));
        }

    }
}