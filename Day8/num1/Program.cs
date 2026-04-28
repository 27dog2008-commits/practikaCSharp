using System;

class Num1
{
    static void Main()
    {
        BrowserHistory browser = new BrowserHistory();

        browser.Visit(new Page { Title = "Главная", Url = "google.com" });
        browser.Visit(new Page { Title = "Новости IT", Url = "habr.com" });
        browser.Visit(new Page { Title = "Прогноз погоды", Url = "weather.com" });

        browser.ShowHistory();
        browser.FindPages("Новости");

        Console.WriteLine("\nНажимаем кнопку 'Назад'...");
        Page previous = browser.GoBack();
        if (previous != null)
        {
            Console.WriteLine($"Вы вернулись на: {previous.Title}");
        }
    }
}