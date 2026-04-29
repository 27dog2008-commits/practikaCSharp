public class USTax : ITaxStrategy
{
    public double CalculateTax(double amount) => amount * 0.1; 
}
