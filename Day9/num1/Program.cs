class Num1
{
    static void Main()
    {
        string fileName = "Matveev.ii";
        string copyName = "Matveev_copy.ii";
        string renameName = "Matveev.io";
        FileManager fm = new FileManager();
        FileInfoProvider info = new FileInfoProvider();

        fm.CreateAndWrite(fileName, "ну типо ку наверное");
        Console.WriteLine("Содержимое: " + File.ReadAllText(fileName));
        info.PrintInfo(fileName);

        fm.CopyFile(fileName, copyName);
        Console.WriteLine($"Копия существует: {File.Exists(copyName)}");

        fm.MoveFile(copyName, renameName);
        Console.WriteLine($"Переименован в: {renameName}");

        long size1 = new FileInfo(fileName).Length;
        long size2 = new FileInfo(renameName).Length;
        Console.WriteLine(size1 == size2 ? "Файлы равны по размеру" : "Разные размеры");

        string[] files = Directory.GetFiles(".", "*.ii");
        foreach (var f in files)
        {
            fm.DeleteFile(f);
        }

        Console.WriteLine("Очистка завершена.");
    }
}