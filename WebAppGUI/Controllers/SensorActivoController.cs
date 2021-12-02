using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppGUI.Data;
using WebAppGUI.Modelos;
using WebAppGUI.Services;

namespace WebAppGUI.Controllers
{
       public class SensorActivoController : Controller
    {
        public ActionResult Index()
        {
            var jsonListaSensorActivo = ApiGatewayClient.MakeGet("sensorActivo/getall");
            List<SensorActivo> lista = JsonConvert.DeserializeObject<List<SensorActivo>>(jsonListaSensorActivo);

            Logger.GetInstance().WriteLog($"Consultando sensorActivo/getall");
            return View(lista);
        }

        public ActionResult Details(int id)
        {
            var jsonSensorActivo = ApiGatewayClient.MakeGet($"sensorActivo/get/{id}");
            var entidad = JsonConvert.DeserializeObject<SensorActivo>(jsonSensorActivo);
            
            Logger.GetInstance().WriteLog($"Consultando sensorActivo/get/{id}");
            return View(entidad);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SensorActivo sensorActivo)
        {
            try
            {
                ApiGatewayClient.MakePost($"sensorActivo/create",sensorActivo);
                
                Logger.GetInstance().WriteLog($"Creando sensor activo");
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
            var jsonSensorActivo = ApiGatewayClient.MakeGet($"sensorActivo/get/{id}");
            var entidad = JsonConvert.DeserializeObject<SensorActivo>(jsonSensorActivo);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Edit(SensorActivo sensorActivo)
        {
            try
            {
                ApiGatewayClient.MakePost($"sensorActivo/edit",sensorActivo);
                Logger.GetInstance().WriteLog($"Editando sensor activo");
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
            var jsonSensorActivo = ApiGatewayClient.MakeGet($"sensorActivo/get/{id}");
            var entidad = JsonConvert.DeserializeObject<SensorActivo>(jsonSensorActivo);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Delete(SensorActivo sensorActivo)
        {
            try
            {
                ApiGatewayClient.MakePost($"sensorActivo/delete",sensorActivo);
                
                Logger.GetInstance().WriteLog($"Eliminando sensor activo");
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