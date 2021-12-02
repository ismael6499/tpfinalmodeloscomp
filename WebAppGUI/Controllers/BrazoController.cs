using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppGUI.Data;
using WebAppGUI.Modelos;
using WebAppGUI.Services;

namespace WebAppGUI.Controllers
{
    public class BrazoController : Controller
    {

        public ActionResult Index()
        {
            var jsonListaBrazo = ApiGatewayClient.MakeGet("brazo/getall");
            List<Brazo> lista = JsonConvert.DeserializeObject<List<Brazo>>(jsonListaBrazo);
            Logger.GetInstance().WriteLog("Consultando brazo/getall");
            return View(lista);
        }

        public ActionResult Details(int id)
        {
            var jsonBrazo = ApiGatewayClient.MakeGet($"brazo/get/{id}");
            var entidad = JsonConvert.DeserializeObject<Brazo>(jsonBrazo);
            Logger.GetInstance().WriteLog($"Consultando brazo/get/{id}");
            return View(entidad);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brazo brazo)
        {
            try
            {
                ApiGatewayClient.MakePost($"brazo/create",brazo);
                Logger.GetInstance().WriteLog("Creando brazo");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var jsonBrazo = ApiGatewayClient.MakeGet($"brazo/get/{id}");
            var entidad = JsonConvert.DeserializeObject<Brazo>(jsonBrazo);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Edit(Brazo brazo)
        {
            try
            {
                ApiGatewayClient.MakePost($"brazo/edit",brazo);
                Logger.GetInstance().WriteLog("Editando brazo");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);

                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var jsonBrazo = ApiGatewayClient.MakeGet($"brazo/get/{id}");
            var entidad = JsonConvert.DeserializeObject<Brazo>(jsonBrazo);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Delete(Brazo brazo)
        {
            try
            {
                ApiGatewayClient.MakePost($"brazo/delete",brazo);
                Logger.GetInstance().WriteLog("Eliminando brazo");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);

                return View();
            }
        }
        
        public IActionResult Encender(string url)
        {
            try
            {
                ApiGatewayClient.MakeGet($"encender/{url}");
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);

                return RedirectToAction(nameof(Index));
            }
        }
        
        public IActionResult Apagar(string url)
        {
            try
            {
                ApiGatewayClient.MakeGet($"apagar/{url}");
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);

                return RedirectToAction(nameof(Index));
            }
        }
    }
}