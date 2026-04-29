public class AsiaTax : ITaxStrategy
{
    public double CalculateTax(double amount) => amount * 0.05; 
}
