class Num3
{
    static double Nod(int a, int b)
    {
        if(b == 0)
        {
            return a;
        }
        return Nod(b, a % b);
    }

    static void Main()
    {
        Console.WriteLine(Nod(12, 8));
        Console.WriteLine(Nod(56, 98));
    }
}