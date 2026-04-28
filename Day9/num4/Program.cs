using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = Directory.GetCurrentDirectory();
        FileWatcher watcher = new FileWatcher(path);

        Console.WriteLine($"Слежка за папкой: {path}");
        Console.WriteLine("Нажмите Enter, чтобы выйти...");
        Console.ReadLine();
    }
}