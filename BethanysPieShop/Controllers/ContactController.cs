using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
