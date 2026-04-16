using System;

public class num1
{
    static void Main()
    {
        double pencil = 2.75;
        int pencil_count = 2;
        double copybook = 2.75;
        int copybook_count = 5;
        double summ = (copybook * copybook_count) + (pencil * pencil_count);
        Console.WriteLine("Стоимость покупки: " + summ);
    }
}