public class MobileApp : IWeatherObserver
{
    public void Update(float temperature)
    {
        Console.WriteLine($"[Mobile App] Получено обновление! Сейчас: {temperature}°C");
    }
}
