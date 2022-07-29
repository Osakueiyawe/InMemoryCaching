using INMemoryCaching.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace INMemoryCaching.Services
{
    public class InMemoryUtility: IInMemoryUtility
    {
        private const string filePath = "C://Log//";
        private readonly IMemoryCache _cache;
        private readonly ILogger<InMemoryUtility> _logger;
        public InMemoryUtility(IMemoryCache cache, ILogger<InMemoryUtility> logger)
        {
            _cache = cache;
            _logger = logger;
        }
        public async Task<bool> AddToCache(string key, string text)
        {
            bool result = false;
            try
            {
                var option = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3),
                    //SlidingExpiration = TimeSpan.FromSeconds(100),
                    PostEvictionCallbacks = { new PostEvictionCallbackRegistration { EvictionCallback = ReloadCache } }
                };
                _cache.Set<string>(key, text, option);
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding {text} to cache", ex);
            }          
            
            return result;
        }

        public async Task<string> GetFromCache(string key)
        {
            string result = "";
            try
            {
                if(!_cache.TryGetValue<string>(key, out result))
                {
                    _cache.Set<string>(key, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting value of {key}", ex);
            }         
            
            return result;
        }

        private void ReloadCache(object key, object value, EvictionReason evictionReason, object state)
        {            
            string loc = filePath + DateTime.Today.ToString("dd-MM-yy");
            string path = loc + "/" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
            value = File.ReadAllText(path);
            _cache.Set<string>(key.ToString(), Convert.ToString(value));
        }
    }
}
