class StockMarket
{
    public event PriceChangedHandler StockPriceChanged;

    public void ChangePrice(decimal price)
    {
        Console.WriteLine($"Биржа: Цена акции изменилась до {price}");
        StockPriceChanged?.Invoke(price);
    }
}
