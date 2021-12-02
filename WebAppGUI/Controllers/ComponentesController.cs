using Microsoft.AspNetCore.Mvc;
using WebAppGUI.Data;

namespace WebAppGUI.Controllers
{
    public class ComponentesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            Logger.GetInstance().WriteLog($"Componentes Controller index");
            return View();
        }
    }
}