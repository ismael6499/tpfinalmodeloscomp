using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAppGUI.Data;
using WebAppGUI.Modelos;

namespace WebAppGUI.Controllers
{
    public class LogController : Controller
    {
        public IActionResult Index()
        {
            List<Log> logs = Logger.GetInstance().ReadLogs();
            return View(logs);
        }
    }
}
