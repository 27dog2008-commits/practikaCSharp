public class UnderlineDecorator : TextDecorator
{
    public UnderlineDecorator(IText component) : base(component) { }
    public override string GetContent() => $"<u>{base.GetContent()}</u>";
}
