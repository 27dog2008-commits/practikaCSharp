class NumberProcessor
{
    private StringParser par = new StringParser();

    public void Process(string input)
    {
        try
        {
            int result = par.ParseToInt(input);
            Console.WriteLine($"Число успешно обработано: {result}");
        }
        catch (FormatException ex)
        {
            throw new InvalidNumberException("Не удалось обработать ввод как число.", ex);
        }
    }
}
