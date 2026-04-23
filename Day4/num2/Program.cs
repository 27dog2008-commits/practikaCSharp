class Num2
{
    static void RectPS(double x1, double y1, double x2, double y2, out double P, out double S)
    {
        double width = Math.Abs(x2 - x1); 
        double height = Math.Abs(y2 - y1);

        P = 2 * (width + height);
        S = height * width;
    }

    static void Main()
    {
        RectPS(0, 0, 3, 4, out double P1, out double S1);
        Console.WriteLine($"P1 = {P1}, S1 = {S1}");

        RectPS(1, 2, 5, 7, out double P2, out double S2);
        Console.WriteLine($"P2 = {P2}, S2 = {S2}");

        RectPS(-2, -1, 4, 3, out double P3, out double S3);
        Console.WriteLine($"P3 = {P3}, S3 = {S3}");
    }
}