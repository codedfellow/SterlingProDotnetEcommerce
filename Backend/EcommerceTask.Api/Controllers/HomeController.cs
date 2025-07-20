using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceTask.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return Content("home page", "text/plain");
        }
    }
}
