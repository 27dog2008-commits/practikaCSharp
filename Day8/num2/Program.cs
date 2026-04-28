using System;

class Num2
{
    static void Main()
    {
        StackHistory<string> history = new StackHistory<string>();

        history.AddAction("Написал текст");
        history.AddAction("Изменил шрифт");
        history.AddAction("Вставил картинку");

        history.Undo();
        history.Undo();
    }
}