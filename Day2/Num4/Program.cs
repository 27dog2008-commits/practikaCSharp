using System;

class Num4
{
    static void Main()
    {
        int[,] matrix = new int[10, 12];
        Random rand = new Random();


        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                matrix[i, j] = rand.Next(1000, 10001);
            }
        }

        Console.Write("Введите число для сравнения с доходом в сентябре: ");
        int target = int.Parse(Console.ReadLine());

        int septemberTotal = 0;
        for (int i = 0; i < 10; i++)
        {
            septemberTotal += matrix[i, 8];
        }


        Console.WriteLine($"Общий доход в сентябре: {septemberTotal}");
        if(septemberTotal > target)
        {
            Console.WriteLine("Да, превысил");
        }
        else
        {
            Console.WriteLine("Нет, не превысил");
        }
    }
}