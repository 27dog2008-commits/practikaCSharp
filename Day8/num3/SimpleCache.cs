public class SimpleCache<T> : ICache<T>
{
    private List<T> _storage = new List<T>();

    public void Add(T item) => _storage.Add(item);
    public IEnumerable<T> GetAll() => _storage;
    public void Clear() => _storage.Clear();
}
