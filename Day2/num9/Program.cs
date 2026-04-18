using System;
using System.Text;

class Num9
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder("ку, ворлд!");
        Console.WriteLine("Исходно: " + sb);

        sb.Remove(5, 2); 
        Console.WriteLine("После удаления: " + sb);
    }
}