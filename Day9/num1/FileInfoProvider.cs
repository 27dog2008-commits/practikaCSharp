class FileInfoProvider
{
    public void PrintInfo(string path)
    {
        FileInfo fi = new FileInfo(path);
        if (fi.Exists)
        {
            Console.WriteLine($"Файл: {fi.Name} | Размер: {fi.Length} байт");
            Console.WriteLine($"Создан: {fi.CreationTime} | Изменен: {fi.LastWriteTime}");
        }
    }
}
