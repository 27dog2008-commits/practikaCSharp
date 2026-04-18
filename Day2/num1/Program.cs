using System;

public class Num1
{
    static void Main()
    {
        Console.WriteLine("Массив");
        int[] arr = new int[10];

        double average = 0;

        foreach (int i in arr)
        {
            arr[i] = Random.Shared.Next(1,11);
            Console.Write(arr[i] + " ");
            average += arr[i];
        }
        
        Console.WriteLine("\nСреднее арифметическое: " + average / arr.Length);

    }
}