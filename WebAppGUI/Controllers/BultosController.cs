using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebAppGUI.Data;
using WebAppGUI.Modelos;

namespace WebAppGUI.Controllers
{
    public class BultosController : Controller
    {
        private readonly INotyfService _notyf;

        public BultosController(INotyfService notyf)
        {
            _notyf = notyf;
        }

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
            _notyf.Custom("Bulto enviado!", 5, "#B600FF", "fa fa-paper-plane");
            return View("Index");
        }
    }
}