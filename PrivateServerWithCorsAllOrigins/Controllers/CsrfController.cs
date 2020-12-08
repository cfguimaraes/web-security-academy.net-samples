using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PrivateServerWithCorsAllOrigins.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsrfController : ControllerBase
    {
        private readonly ILogger<CsrfController> _logger;

        public CsrfController(ILogger<CsrfController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string url, [FromQuery] string cookie)
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(cookie)) return BadRequest();

            var webClient = new WebClient();
            webClient.Headers.Add("Cookie", cookie);
            var html = webClient.DownloadString(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var allInputs = doc.DocumentNode.Descendants().Where(x => x.Name == "input");
            var csrfInput = allInputs.FirstOrDefault(x => x.Attributes.Any(y => y.Value == "csrf"));
            if (csrfInput is null) return Conflict();

            return Ok(csrfInput.Attributes["value"].Value);
        }
    }
}
