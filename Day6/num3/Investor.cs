class Investor
{
    public void OnPriceChanged(decimal price)
    {
        Console.WriteLine($"Инвестор: При цене {price} пора покупать!");
    }
}
