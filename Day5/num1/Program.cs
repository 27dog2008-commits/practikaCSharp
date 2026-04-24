class Num1
{
    static void Main()
    {

        PaymentMethod[] methods = new PaymentMethod[]
        {
                new CreditCard(),
                new PayPal(),
                new Cash()
        };

        Console.WriteLine("Обработка платежей:");
        foreach (var method in methods)
        {
            method.Pay(100.50m); 
        }
    }
}