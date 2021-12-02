using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAppGUI.Data;
using WebAppGUI.Modelos;

namespace WebAppGUI.Controllers
{
    public class GraficosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetPiechartJSON()
        {
            var bultoLog = Logger.GetInstance().GetBultoLog();

            List<ChartsModel> list = new List<ChartsModel>();

            list.Add(new ChartsModel { tipo = "Ingresados", cantidad = bultoLog.Ingresados });
            list.Add(new ChartsModel { tipo = "Prensado", cantidad = bultoLog.Prensado });
            list.Add(new ChartsModel { tipo = "Apilados", cantidad = bultoLog.Apilados });

            return Json(new { JSONList = list });
        }
    }
}