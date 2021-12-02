using Microsoft.AspNetCore.Mvc;

namespace WebAppGUI.Controllers
{
    public class ComponentesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}