using System;
using System.Text.RegularExpressions;

class Num10
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();

        bool hasUpperCase = Regex.IsMatch(input, @"[A-ZА-Я]");
        Console.WriteLine(hasUpperCase ? "Содержит заглавные буквы" : "Не содержит заглавных букв");
    }
}