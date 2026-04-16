using System;
public class num2
{
    static void Main()
    {
        int num = Random.Shared.Next(10, 99);
        Console.WriteLine("Число: " + num);
        int num_summ = (num % 10) + (num / 10);
        Console.WriteLine("Сумма его цифр: " + num_summ);
    }
}