using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppGUI.Data;
using WebAppGUI.Modelos;
using WebAppGUI.Services;

namespace WebAppGUI.Controllers
{
    public class SensorPasivoController : Controller
    {
         public ActionResult Index()
        {
            var jsonListaSensorPasivo = ApiGatewayClient.MakeGet("sensorPasivo/getall");
            List<SensorPasivo> lista = JsonConvert.DeserializeObject<List<SensorPasivo>>(jsonListaSensorPasivo);
            
            Logger.GetInstance().WriteLog($"Consultando sensorPasivo/getall");

            return View(lista);
        }

        public ActionResult Details(int id)
        {
            var jsonSensorPasivo = ApiGatewayClient.MakeGet($"sensorPasivo/get/{id}");
            var entidad = JsonConvert.DeserializeObject<SensorPasivo>(jsonSensorPasivo);
            
            Logger.GetInstance().WriteLog($"Consultando sensorPasivo/get/{id}");
            return View(entidad);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SensorPasivo sensorPasivo)
        {
            try
            {
                ApiGatewayClient.MakePost($"sensorPasivo/create",sensorPasivo);
                
                Logger.GetInstance().WriteLog($"Creando sensor pasivo");
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
            var jsonSensorPasivo = ApiGatewayClient.MakeGet($"sensorPasivo/get/{id}");
            var entidad = JsonConvert.DeserializeObject<SensorPasivo>(jsonSensorPasivo);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Edit(SensorPasivo sensorPasivo)
        {
            try
            {
                ApiGatewayClient.MakePost($"sensorPasivo/edit",sensorPasivo);
                
                Logger.GetInstance().WriteLog($"Editando sensor pasivo");
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
            var jsonSensorPasivo = ApiGatewayClient.MakeGet($"sensorPasivo/get/{id}");
            var entidad = JsonConvert.DeserializeObject<SensorPasivo>(jsonSensorPasivo);
            
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Delete(SensorPasivo sensorPasivo)
        {
            try
            {
                ApiGatewayClient.MakePost($"sensorPasivo/delete",sensorPasivo);
                
                Logger.GetInstance().WriteLog($"Eliminando sensor pasivo");
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