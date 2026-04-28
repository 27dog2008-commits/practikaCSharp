using System;
using System.Collections.Generic;

class Num3
{
    static void Main()
    {
        var stringCache = new SimpleCache<string>();
        var manager = new CacheManager<string>(stringCache);

        manager.AddToCache("Данные пользователя #1");
        manager.AddToCache("Настройки профиля");
        manager.AddToCache("Список друзей");

        manager.PrintCache();
        Console.WriteLine($"Всего элементов: {manager.GetCount()}");

        var intCache = new SimpleCache<int>();
        var intManager = new CacheManager<int>(intCache);
        intManager.AddToCache(404);
        intManager.AddToCache(200);
    }
}