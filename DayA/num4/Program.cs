using System;
public class num4
{
    static void Main()
    {
        Console.Write("Введите первое число: ");
        double num1 = double.Parse(Console.ReadLine());

        Console.Write("Введите второе число: ");
        double num2 = double.Parse(Console.ReadLine());

        double summ = num1 * num2;

        Console.WriteLine($"Произведение чисел: {summ:F1}");
    }
}