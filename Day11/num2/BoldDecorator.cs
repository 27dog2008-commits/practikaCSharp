public class BoldDecorator : TextDecorator
{
    public BoldDecorator(IText component) : base(component) { }
    public override string GetContent() => $"<b>{base.GetContent()}</b>";
}
