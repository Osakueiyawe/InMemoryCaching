using INMemoryCaching.Interfaces;
using INMemoryCaching.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INMemoryCaching.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileUtility _fileUtility;
        public FileController(IFileUtility fileUtility)
        {
            _fileUtility = fileUtility;
        }
        [HttpPost]
        public async Task<IActionResult> AddText([FromBody] string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return Ok(await _fileUtility.WriteToFile(text));
            }
            return BadRequest();
        }

        [HttpGet("{key}")]        
        public async Task<IActionResult> GetText([FromRoute] string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return Ok(await _fileUtility.GetFromFile(key));
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetHeader([FromQuery] string query, [FromHeader] Header headers)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Ok(headers);
            }
            return BadRequest();
        }
    }
}
