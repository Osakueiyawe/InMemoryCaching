using INMemoryCaching.Interfaces;
using System.IO;
using System.Reflection;

namespace INMemoryCaching.Services
{
    public class FileUtility:IFileUtility
    {
        private const string filePath = "C://Log//";
        private readonly IInMemoryUtility _inMemoryUtility;
        private readonly ILogger<FileUtility> _logger;
        public FileUtility(IInMemoryUtility inMemoryUtility, ILogger<FileUtility> logger)
        {
            _logger = logger;
            _inMemoryUtility = inMemoryUtility;
        }
        public async Task<bool> WriteToFile(string text)
        {
            bool result = false;
            try
            {
                string loc = filePath + DateTime.Today.ToString("dd-MM-yy");                
                
                if (!Directory.Exists(loc))
                {
                    Directory.CreateDirectory(loc);
                }
                string path = loc + "/" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
                File.WriteAllText(path, text);
                _logger.LogInformation($"{text} has been written to the Text file");
                bool isCacheAdded = await _inMemoryUtility.AddToCache("FileText", text);
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Error writing {text}: ",ex);
            }
            return result;
        }

        public async Task<string> GetFromFile(string key)
        {
            string result = "";
            try
            {
                result = await _inMemoryUtility.GetFromCache(key);
                if (string.IsNullOrEmpty(result))
                {
                    string loc = filePath + DateTime.Today.ToString("dd-MM-yy");
                    string path = loc + "/" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
                    result = File.ReadAllText(path);
                    _logger.LogInformation($"{result} was fetched from the file");
                }                                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting text for {key}", ex);
            }
            return result;
        }
    }
}
