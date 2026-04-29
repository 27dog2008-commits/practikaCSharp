public class EUTax : ITaxStrategy
{
    public double CalculateTax(double amount) => amount * 0.2; 
}
