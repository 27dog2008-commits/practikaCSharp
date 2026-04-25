public delegate void LogHandler(string message);

class Num2
{
    static void Main()
    {
        string logData = "Система запущена успешно.";


        Logger.LogMessage(logData, Logger.ConsoleLog);
        Logger.LogMessage(logData, Logger.FileLog);
    }
}