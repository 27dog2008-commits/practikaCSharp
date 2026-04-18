using System;

public class Num2
{
    static void Main()
    {
        Console.WriteLine("Массив");
        int n = 11;
        double[] arr = new double[n];

        double average = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = Random.Shared.Next(-10, 11);
            Console.Write(arr[i] + " ");
            average += arr[i];
        }

        average /= arr.Length;
        Console.WriteLine();
        Console.WriteLine($"Среднее арифметическое: {average:F1}");

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < 0)
            {
                arr[i] += 0.5;
            }
            else if (arr[i] >= 0 && arr[i] < average)
            {
                arr[i] = 0.1;
            }
        }

        Console.WriteLine("Преобразованный массив");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();

        Array.Sort(arr);
        Console.WriteLine("Отсортированный массив");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();

        Console.Write("Введите число k для бинарного поиска: ");
        double k = double.Parse(Console.ReadLine());

        int index = Array.BinarySearch(arr, k);

        if (index >= 0)
        {
            Console.WriteLine($"Число {k} найдено на позиции {index}");
        }
        else
        {
            Console.WriteLine($"Число {k} не найдено");
        }
    }
}