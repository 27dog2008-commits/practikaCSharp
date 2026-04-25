public delegate void PriceChangedHandler(decimal newPrice);

class Num3
{
    static void Main()
    {
        StockMarket market = new StockMarket();
        Investor investor = new Investor();
        NewsAgency news = new NewsAgency();

        market.StockPriceChanged += investor.OnPriceChanged;
        market.StockPriceChanged += news.ReportPrice;

        market.ChangePrice(150.75m);
    }
}