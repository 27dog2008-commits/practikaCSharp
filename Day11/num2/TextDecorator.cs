public abstract class TextDecorator : IText
{
    protected IText _textComponent;
    public TextDecorator(IText component) => _textComponent = component;
    public virtual string GetContent() => _textComponent.GetContent();
}
