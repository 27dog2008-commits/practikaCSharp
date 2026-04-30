class Num2
{
    static void Main(string[] args)
    {
        Console.WriteLine("Паттерн Декоратор");

        IText myText = new PlainText("текст для работы");
        Console.WriteLine("Обычный: " + myText.GetContent());

        IText boldText = new BoldDecorator(myText);
        IText richText = new ItalicDecorator(boldText);
        IText fullyDecorated = new UnderlineDecorator(richText);

        Console.WriteLine("Стилизованный: " + fullyDecorated.GetContent());
    }
}