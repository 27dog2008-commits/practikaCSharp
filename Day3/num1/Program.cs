using System;
using System.Runtime.CompilerServices;

public class A
{
    public int a;
    public int b; 

    public A(int a, int b)
    {
        this.a = a;
        this.b = b;
    }

    public double MyPow()
    {
        double number = a + b;
        double answer = Math.Pow(number, 3);
        return answer;
    }

    public double Quotient()
    {
        double answer = a / b;
        return answer;
    }

    static void Main()
    {
        A obj = new A(10,5);
        Console.WriteLine("a = " + obj.a + "\nb = " + obj.b);
        Console.WriteLine("Частное: " + obj.Quotient());
        Console.WriteLine("Ответ уравнение: " + obj.MyPow());
    }

}       