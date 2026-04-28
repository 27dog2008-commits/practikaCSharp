public class MyStack<T>
{
    private T[] _items;
    private int _count;
    private const int DefaultCapacity = 4;

    public int Count => _count;

    public MyStack()
    {
        _items = new T[DefaultCapacity];
    }

    public void Push(T item)
    {
        if (_count == _items.Length)
        {
            Array.Resize(ref _items, _items.Length * 2); 
        }

        _items[_count++] = item;
    }

    public T Pop()
    {
        if (_count == 0)
        {
            throw new InvalidOperationException("Стек пуст");
        }
        T item = _items[--_count];
        _items[_count] = default;
        return item;
    }

    public T Peek()
    {
        if (_count == 0)
        {
            throw new InvalidOperationException("Стек пуст");
        }
        return _items[_count - 1];
    }
}
