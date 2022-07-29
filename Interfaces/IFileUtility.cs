namespace INMemoryCaching.Interfaces
{
    public interface IFileUtility
    {
        Task<bool> WriteToFile(string text);
        Task<string> GetFromFile(string key);
    }
}
