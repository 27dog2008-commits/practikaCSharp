class Num1
{
    static void Main()
    {
        PasswordValidator validator = new PasswordValidator();

        try
        {
            Console.Write("Введите новый пароль: ");
            string input = Console.ReadLine();
            validator.ValidatePassword(input);
        }
        catch (WeakPasswordException ex)
        {

            Console.WriteLine($"[Обработка ошибки]: {ex.Message}");
        }
    }
}