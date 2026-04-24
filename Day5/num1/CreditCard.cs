class CreditCard : PaymentMethod
{
    public override void Pay(decimal amount) =>
        Console.WriteLine($"Оплата {amount:C} успешно проведена через Credit Card.");
}
