class Logger
{
    public static void ConsoleLog(string msg)
    {
        Console.WriteLine($"консоль: {msg}");
    }

    public static void FileLog(string msg)
    {
        Console.WriteLine($"файл: {msg} (запись в файл имитирована)");
    }


    public static void LogMessage(string message, LogHandler handler)
    {
        handler?.Invoke(message);
    }
}
