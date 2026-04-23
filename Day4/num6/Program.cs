using System;

class Program
{
    static int MonthDays(int M, int Y)
    {
        if (M < 1 || M > 12)
        {
            return 0;
        }

        if (M == 1 || M == 3 || M == 5 || M == 7 || M == 8 || M == 10 || M == 12)
        {
            return 31;
        }

        if (M == 4 || M == 6 || M == 9 || M == 11)
        {
            return 30;
        }

        if (IsLeapYear(Y))
        {
            return 29;
        }
        else
        {
            return 28;
        }
    }
    static bool IsLeapYear(int Y)
    {
        return (Y % 4 == 0 && Y % 100 != 0) || (Y % 400 == 0);
    }

    static void Main()
    {
        int Y = 2024;
        int M1 = 2, M2 = 4, M3 = 12;

        Console.WriteLine(MonthDays(M1, Y)); 
        Console.WriteLine(MonthDays(M2, Y)); 
        Console.WriteLine(MonthDays(M3, Y)); 
    }
}