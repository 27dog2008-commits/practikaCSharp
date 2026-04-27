public class WeakPasswordException : Exception
{
    public WeakPasswordException() : base("Пароль слишком слабый.") { }

    public WeakPasswordException(string message) : base(message) { }

    public WeakPasswordException(string message, Exception inner) : base(message, inner) { }
}
