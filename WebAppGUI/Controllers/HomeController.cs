using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAppGUI.Modelos;
using WebAppGUI.Models;

namespace WebAppGUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly INotyfService _notyf;
        public HomeController(INotyfService notyf)
        {
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            ViewBag.isHome = true;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public void Notify()
        {
            var rnd = new Random();
            _notyf.Success(Constantes.frases[rnd.Next(0, Constantes.frases.Length)] + "!");
        }       
    }
}