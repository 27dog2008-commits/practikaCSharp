using System.Collections;

public class BrowserHistory
{
    private Stack _history = new Stack();

    public void Visit(Page page)
    {
        _history.Push(page);
        Console.WriteLine($"Переход на: {page.Title}");
    }

    public Page GoBack()
    {
        if (_history.Count > 1)
        {
            _history.Pop();
            return (Page)_history.Peek(); 
        }
        return null;
    }

    public void ShowHistory()
    {
        Console.WriteLine("\nИстория посещений (от новых к старым):");
        foreach (var item in _history)
        {
            Page p = (Page)item; 
            Console.WriteLine(p);
        }
    }

    public void FindPages(string query)
    {
        Console.WriteLine($"\nРезультаты поиска по запросу '{query}':");
        foreach (var item in _history)
        {
            Page p = (Page)item;
            if (p.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Найдено: {p}");
            }
        }
    }
}
