using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppGUI.Data;
using WebAppGUI.Modelos;
using WebAppGUI.Services;

namespace WebAppGUI.Controllers
{
    public class CintaController : Controller
    {
        public ActionResult Index()
        {
            var jsonListaCinta = ApiGatewayClient.MakeGet("cinta/getall");
            List<Cinta> lista = JsonConvert.DeserializeObject<List<Cinta>>(jsonListaCinta);

            Logger.GetInstance().WriteLog("Consultando cinta/getall");
            return View(lista);
        }

        public ActionResult Details(int id)
        {
            var jsonCinta = ApiGatewayClient.MakeGet($"cinta/get/{id}");
            var entidad = JsonConvert.DeserializeObject<Cinta>(jsonCinta);
            Logger.GetInstance().WriteLog($"Consultando cinta/get/{id}");

            return View(entidad);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cinta cinta)
        {
            try
            {
                ApiGatewayClient.MakePost($"cinta/create", cinta);
                Logger.GetInstance().WriteLog($"Creando cinta");

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
            var jsonCinta = ApiGatewayClient.MakeGet($"cinta/get/{id}");
            var entidad = JsonConvert.DeserializeObject<Cinta>(jsonCinta);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Edit(Cinta cinta)
        {
            try
            {
                ApiGatewayClient.MakePost($"cinta/edit", cinta);
                Logger.GetInstance().WriteLog($"Editando cinta");
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
            var jsonCinta = ApiGatewayClient.MakeGet($"cinta/get/{id}");
            var entidad = JsonConvert.DeserializeObject<Cinta>(jsonCinta);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Delete(Cinta cinta)
        {
            try
            {
                ApiGatewayClient.MakePost($"cinta/delete", cinta);
                Logger.GetInstance().WriteLog($"Eliminando cinta");
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