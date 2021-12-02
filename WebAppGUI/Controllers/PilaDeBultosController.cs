using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppGUI.Data;
using WebAppGUI.Modelos;
using WebAppGUI.Services;

namespace WebAppGUI.Controllers
{
    public class PilaDeBultosController : Controller
    {
          public ActionResult Index()
        {
            var jsonListaPilaDeBultos = ApiGatewayClient.MakeGet("pilaDeBultos/getall");
            List<PilaDeBultos> lista = JsonConvert.DeserializeObject<List<PilaDeBultos>>(jsonListaPilaDeBultos);

            Logger.GetInstance().WriteLog($"Consultando pilaDeBultos/getall");

            return View(lista);
        }

        public ActionResult Details(int id)
        {
            var jsonPilaDeBultos = ApiGatewayClient.MakeGet($"pilaDeBultos/get/{id}");
            var entidad = JsonConvert.DeserializeObject<PilaDeBultos>(jsonPilaDeBultos);
            
            Logger.GetInstance().WriteLog($"Consultando pilaDeBultos/get/{id}");
            return View(entidad);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PilaDeBultos pilaDeBultos)
        {
            try
            {
                ApiGatewayClient.MakePost($"pilaDeBultos/create", pilaDeBultos);
                
                Logger.GetInstance().WriteLog($"Creando pila de bultos");
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
            var jsonPilaDeBultos = ApiGatewayClient.MakeGet($"pilaDeBultos/get/{id}");
            var entidad = JsonConvert.DeserializeObject<PilaDeBultos>(jsonPilaDeBultos);
            
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Edit(PilaDeBultos pilaDeBultos)
        {
            try
            {
                ApiGatewayClient.MakePost($"pilaDeBultos/edit", pilaDeBultos);
                
                Logger.GetInstance().WriteLog($"Editando pila de bultos");
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
            var jsonPilaDeBultos = ApiGatewayClient.MakeGet($"pilaDeBultos/get/{id}");
            var entidad = JsonConvert.DeserializeObject<PilaDeBultos>(jsonPilaDeBultos);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Delete(PilaDeBultos pilaDeBultos)
        {
            try
            {
                ApiGatewayClient.MakePost($"pilaDeBultos/delete", pilaDeBultos);
                
                Logger.GetInstance().WriteLog($"Eliminando pila de bultos");
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