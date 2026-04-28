public class CacheManager<T>
{
    private readonly ICache<T> _cache;

    public CacheManager(ICache<T> cache)
    {
        _cache = cache;
    }

    public void AddToCache(T item) => _cache.Add(item);

    public void PrintCache()
    {
        Console.WriteLine("Содержимое кэша:");
        foreach (var item in _cache.GetAll())
        {
            Console.WriteLine($"- {item}");
        }
    }

    public int GetCount()
    {
        int count = 0;
        foreach (var _ in _cache.GetAll()) count++;
        return count;
    }
}
