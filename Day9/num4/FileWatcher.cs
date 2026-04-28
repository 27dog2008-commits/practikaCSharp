class FileWatcher
{
    private FileSystemWatcher _watcher;
    private string _logPath = "log.txt";

    public FileWatcher(string directory)
    {
        _watcher = new FileSystemWatcher(directory);


        _watcher.Renamed += OnRenamed;
        _watcher.Created += (s, e) => Console.WriteLine($"Создан: {e.Name}");
        _watcher.Deleted += (s, e) => Console.WriteLine($"Удален: {e.Name}");

        _watcher.EnableRaisingEvents = true;
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        string message = $"Файл {e.OldName} переименован в {e.Name}";
        Console.WriteLine(message);

        File.AppendAllText(_logPath, $"{DateTime.Now}: {e.OldFullPath} -> {e.FullPath}{Environment.NewLine}");
    }
}
