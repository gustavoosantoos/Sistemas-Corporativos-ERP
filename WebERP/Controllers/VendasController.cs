using Microsoft.AspNetCore.Mvc;

namespace WebERP.Controllers
{
    public class VendasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}