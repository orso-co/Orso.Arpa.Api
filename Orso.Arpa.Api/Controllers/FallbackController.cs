using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Orso.Arpa.Api.Controllers
{
    [AllowAnonymous]
    public class FallbackController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "index.html"), "text/HTML");
        }
    }
}
