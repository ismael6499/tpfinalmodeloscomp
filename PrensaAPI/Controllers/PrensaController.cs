using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using PrensaAPI.Data;
using PrensaAPI.Modelos;

namespace PrensaAPI.Controllers
{
    [ApiController]
    public class PrensaController : ControllerBase
    {
        private readonly PrensaService _prensaService;
        private readonly Control control;

        public PrensaController(PrensaService prensaService, Control control)
        {
            this._prensaService = prensaService;
            this.control = control;
        }

        [HttpGet]
        [Route("/api/estado")]
        public string Estado()
        {
            Logger.GetInstance().WriteLog("Verificando estado de la prensa");
            string jsonSeñal = _prensaService.verificarEstado("$levantado$");
            Logger.GetInstance().WriteLog(jsonSeñal);
            return jsonSeñal;
        }

        [HttpGet]
        [Route("/api/libre")]
        public bool Libre()
        {
            Logger.GetInstance().WriteLog("Consultando si la prensa está libre");
            bool isLibre = _prensaService.consultarSensorLibre();
            return isLibre;
        }

        [HttpPost]
        [Route("/api/prensar")]
        public async Task<string> Prensar([FromBody] object body)
        {
            string jsonString = body.ToString();
            Bulto bulto = JsonConvert.DeserializeObject<Bulto>(jsonString);
            Bulto bultoPrensado = await _prensaService.Prensar(bulto);
            string msjAgregadoPila  = control.llevarBultoALaPila(bultoPrensado);
            string respuesta = bultoPrensado.GlobalId + " - Prensado - " + msjAgregadoPila;
            Logger.GetInstance().WriteLog(respuesta);
            return respuesta;
        }
    }
}