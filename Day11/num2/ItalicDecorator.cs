public class ItalicDecorator : TextDecorator
{
    public ItalicDecorator(IText component) : base(component) { }
    public override string GetContent() => $"<i>{base.GetContent()}</i>";
}
