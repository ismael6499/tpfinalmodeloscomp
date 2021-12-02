using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppGUI.Controllers
{
    public class GraficosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
