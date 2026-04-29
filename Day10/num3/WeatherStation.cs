public class WeatherStation
{
    private List<IWeatherObserver> _observers = new List<IWeatherObserver>();
    private float _temperature;

    public void RegisterObserver(IWeatherObserver observer) 
    { 
        _observers.Add(observer); 
    }

    public void RemoveObserver(IWeatherObserver observer)
    {
        _observers.Remove(observer);
    }

    public void SetTemperature(float temp)
    {
        Console.WriteLine($"\n[Метеостанция] Температура изменилась на {temp}°C. Оповещаю всех...");
        _temperature = temp;
        NotifyObservers();
    }

    private void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_temperature);
        }
    }
}

