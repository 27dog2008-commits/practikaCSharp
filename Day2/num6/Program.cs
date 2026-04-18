using System;
using System.Text.RegularExpressions;

class Num6
{
    static void Main()
    {
        string text = "Приве, это текст; привет: да !";
        Console.WriteLine("Исходная строка: " + text);

        string result = Regex.Replace(text, @"[^\w\s]", "");
        Console.WriteLine("После удаления знаков препинания: " + result);
    }
}