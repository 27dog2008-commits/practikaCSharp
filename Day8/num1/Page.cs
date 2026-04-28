public class Page
{
    public string Url { get; set; }
    public string Title { get; set; }

    public override string ToString() => $"[{Title}] ({Url})";
}