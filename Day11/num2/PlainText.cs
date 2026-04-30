public class PlainText : IText
{
    private string _text;
    public PlainText(string text) => _text = text;
    public string GetContent() => _text;
}
