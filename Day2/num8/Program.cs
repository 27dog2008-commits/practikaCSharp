using System;
using System.Text.RegularExpressions;

class Num8
{
    static void Main()
    {
        string id = "myId";
        string id_2 = "нет";
        bool isValid = Regex.IsMatch(id, @"^[a-zA-Z_][a-zA-Z0-9_]*$");
        bool isValid_2 = Regex.IsMatch(id_2, @"^[a-zA-Z_][a-zA-Z0-9_]*$");
        Console.WriteLine(isValid ? "Допустимый идентификатор" : "Недопустимый идентификатор");
        Console.WriteLine(isValid_2 ? "Допустимый идентификатор" : "Недопустимый идентификатор");
    }
}