public class PasswordValidator
{
    public void ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8)
        {
            throw new WeakPasswordException("Ошибка: Пароль должен содержать минимум 8 символов!");
        }
        Console.WriteLine("Пароль принят.");
    }
}
