using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppGUI.Data;
using WebAppGUI.Modelos;

namespace WebAppGUI.Controllers
{
    public class BultosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PonerBulto(Bulto bulto)
        {
            var urlApiGateway = Program.configuration["urls:ApiGateway"];
            string responseAsString = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(urlApiGateway);
                var response = await httpClient.PostAsJsonAsync("ponerbulto", bulto);
                responseAsString = await response.Content.ReadAsStringAsync();
                responseAsString = "Bulto enviando a cinta";
                Logger.GetInstance().WriteLog("Bulto enviando a cinta");
            }
            ViewBag.response = responseAsString;
            return View("Index");
        }
    }
}