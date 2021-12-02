using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAppGUI.Modelos;
using WebAppGUI.Models;

namespace WebAppGUI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> PonerBulto()
        {
            String responseToView = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:22222/v1/");
                Bulto bulto = new Bulto();
                bulto.Descripcion = "testing";
                var response = await httpClient.PostAsJsonAsync("ponerbulto",bulto );
                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    responseToView = responseAsString;
                }
                else
                {
                    responseToView = "Error al consultar";
                }
            }

            ViewBag.response = responseToView;
            return View("Index");
        }
    }
}