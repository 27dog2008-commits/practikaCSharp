class FileManager
{
    public void CreateAndWrite(string path, string content) => File.WriteAllText(path, content);

    public void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Console.WriteLine($"Ошибка: Файл {path} не найден для удаления.");
        }
    }

    public void CopyFile(string source, string dest) => File.Copy(source, dest, true);

    public void MoveFile(string source, string dest)
    {
        if (File.Exists(dest))
        {
            File.Delete(dest);
        }
        File.Move(source, dest);
    }

    public void SetReadOnly(string path, bool readOnly)
    {
        FileInfo fi = new FileInfo(path);
        fi.IsReadOnly = readOnly;
    }
}
