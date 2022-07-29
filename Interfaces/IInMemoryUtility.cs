namespace INMemoryCaching.Interfaces
{
    public interface IInMemoryUtility
    {
        Task<bool> AddToCache(string key, string text);
        Task<string> GetFromCache(string key);
    }
}
