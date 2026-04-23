using System;

class Program
{
    static string GetDescription(int number)
    {
        switch (number)
        {
            case 1: 
                return "один";
            case 2:
                return "два";
            case 3:
                return "три";
            case 4:
                return "четыре";
            default:
                return "число вне диапазона (1-4)";
        }
    }

    static string GetDescription(int number, bool Yes)  
    {
        switch (number)
        {
            case 5: return "пять";
            case 6: return "шесть";
            case 7: return "семь";
            case 8: return "восемь";
            case 9: return "девять";
            case 10: return "десять";
            default: return "число вне диапазона (5-10)";
        }
    }

    static void Main()
    {
        Console.WriteLine(GetDescription(1));
        Console.WriteLine(GetDescription(3)); 
        Console.WriteLine(GetDescription(5, true));
        Console.WriteLine(GetDescription(7, true)); 
    }
}