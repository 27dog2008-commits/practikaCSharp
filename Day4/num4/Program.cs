public static class Num4
{
    static string DelSpace(this string str)
    {
        return str.Replace(" ", "");
    }

    static void Main()
    {
        string str = "строка для примера, типо нверное эээ";
        Console.WriteLine("Пример строки: " + str);
        Console.WriteLine("Вывод: " + str.DelSpace());
    }
}