using Microsoft.AspNetCore.Mvc;

namespace buoi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
