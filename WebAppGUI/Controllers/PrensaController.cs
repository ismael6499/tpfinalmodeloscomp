using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppGUI.Data;
using WebAppGUI.Modelos;
using WebAppGUI.Services;

namespace WebAppGUI.Controllers
{
    public class PrensaController : Controller
    {
        public ActionResult Index()
        {
            var jsonListaPrensa = ApiGatewayClient.MakeGet("prensa/getall");
            List<Prensa> lista = JsonConvert.DeserializeObject<List<Prensa>>(jsonListaPrensa);
            
            Logger.GetInstance().WriteLog($"Consultando prensa/getall");

            return View(lista);
        }

        public ActionResult Details(int id)
        {
            var jsonPrensa = ApiGatewayClient.MakeGet($"prensa/get/{id}");
            
            Logger.GetInstance().WriteLog($"Consultando prensa/get/{id}");
            var entidad = JsonConvert.DeserializeObject<Prensa>(jsonPrensa);
            return View(entidad);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Prensa prensa)
        {
            try
            {
                ApiGatewayClient.MakePost($"prensa/create",prensa);
                
                Logger.GetInstance().WriteLog($"Creando prensa");
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
            var jsonPrensa = ApiGatewayClient.MakeGet($"prensa/get/{id}");
            var entidad = JsonConvert.DeserializeObject<Prensa>(jsonPrensa);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Edit(Prensa prensa)
        {
            try
            {
                ApiGatewayClient.MakePost($"prensa/edit",prensa);
                
                Logger.GetInstance().WriteLog($"Editando prensa");
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
            var jsonPrensa = ApiGatewayClient.MakeGet($"prensa/get/{id}");
            var entidad = JsonConvert.DeserializeObject<Prensa>(jsonPrensa);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Delete(Prensa prensa)
        {
            try
            {
                ApiGatewayClient.MakePost($"prensa/delete",prensa);
                
                Logger.GetInstance().WriteLog($"Eliminando prensa");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);

                return View();
            }
        }
    }
}