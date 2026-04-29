using System;

public class DatabaseConnection
{
    private static DatabaseConnection _instance;

    private static readonly object _lock = new object();

    private readonly string _databaseName = "NewspaperDistribution";

    private DatabaseConnection()
    {
        Console.WriteLine($"[System] Инициализация системы для базы: {_databaseName}");
    }

    public static DatabaseConnection GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new DatabaseConnection();
                }
            }
        }
        return _instance;
    }

    public void Connect()
    {
        Console.WriteLine($"[Connected] Успешное подключение к базе данных: {_databaseName}");
    }

    public void Disconnect()
    {
        Console.WriteLine($"[Disconnected] Соединение с {_databaseName} разорвано.");
    }

    public void ExecuteQuery(string query)
    {
        Console.WriteLine($"[Query] В базе {_databaseName} выполнен SQL-запрос: \"{query}\"");
    }
}
