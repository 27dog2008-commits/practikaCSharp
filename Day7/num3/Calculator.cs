public class Calculator
{
    public int Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivisionByZeroException($"Попытка деления числа {a} на ноль недопустима.");
        }
        return a / b;
    }
}
