public class Website : IWeatherObserver
{
    public void Update(float temperature)
    {
        Console.WriteLine($"[Website] Обновление данных и виджета на главной: {temperature}C");
    }
}
