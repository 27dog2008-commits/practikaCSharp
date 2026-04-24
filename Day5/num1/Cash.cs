class Cash : PaymentMethod
{
    public override void Pay(decimal amount) =>
        Console.WriteLine($"Оплата {amount:C} наличными принята.");
}
