class Num1
{
    static void Main()
    {
        DatabaseConnection db = DatabaseConnection.GetInstance();

        db.Connect();

        db.ExecuteQuery("SELECT * FROM DeliveryRoutes");

        db.Disconnect();

        DatabaseConnection secondDb = DatabaseConnection.GetInstance();
        if (ReferenceEquals(db, secondDb))
        {
            Console.WriteLine("\nОба объекта — это один и тот же экземпляр. Паттерн работает");
        }
    }
}