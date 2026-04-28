public class StackHistory<T>
{
    private MyStack<T> _stack = new MyStack<T>();

    public void AddAction(T action)
    {
        _stack.Push(action);
        Console.WriteLine($"Действие '{action}' сохранено.");
    }

    public void Undo()
    {
        if (_stack.Count > 0)
        {
            Console.WriteLine($"Действие '{_stack.Pop()}' отменено.");
        }
    }
}
