using System;

public class Num6
{
    static void Main()
    {
        Console.WriteLine("a - автомобиль \nв - велосипед, \nм - мотоцикл, \nс - самолет, \nп - поезд.");
        Console.Write("Введите букву для выбора транспорта: ");
        char letter = Console.ReadKey().KeyChar;
        Console.WriteLine();

        switch (letter)
        {
            case 'а':
                Console.WriteLine("Максимальная скорость: 60");
                break;
            case 'в':
                Console.WriteLine("Максимальная скорость: 30");
                break;
            case 'м':
                Console.WriteLine("Максимальная скорость: 100");
                break;
            case 'с':
                Console.WriteLine("Максимальная скорость: ну тут очень много");
                break;
            case 'п':
                Console.WriteLine("Максимальная скорость: 90");
                break;
            default:
                Console.WriteLine("Ошибка ввода");
                break;
        }
    }
}