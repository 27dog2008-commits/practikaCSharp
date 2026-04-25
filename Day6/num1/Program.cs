public delegate string TextProcessor(string input);

class Num1
{
    static void Main()
    {
        UpperCaseConverter upper = new UpperCaseConverter();
        LowerCaseConverter lower = new LowerCaseConverter();

        TextProcessor processor = upper.ToUpper;
        Console.WriteLine($"Результат (Upper): {processor("ку")}");

        processor = lower.ToLower;
        Console.WriteLine($"Результат (Lower): {processor("КУ")}");
    }
}