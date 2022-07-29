using Microsoft.AspNetCore.Mvc;

namespace INMemoryCaching.Models
{
    public class Header
    {
        [FromHeader]
        public string Content { get; set; }
    }
}
