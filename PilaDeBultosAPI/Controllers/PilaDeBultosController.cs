using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PilaDeBultosAPI.Controllers
{
    [ApiController]
    public class PilaDeBultosController : ControllerBase
    {
        private readonly PilaDeBultosService _pilaDeBultosService;

        public PilaDeBultosController(PilaDeBultosService pilaDeBultosService)
        {
            this._pilaDeBultosService = pilaDeBultosService;
        }

        [HttpPost]
        [Route("/api/agregarbulto")]
        public string AgregarBulto([FromBody] object body)
        {
            string jsonBody = body.ToString();
            try
            {
                Bulto bulto = JsonConvert.DeserializeObject<Bulto>(jsonBody);
                _pilaDeBultosService.agregarBulto(bulto);
                return "ok";
            }
            catch (Exception e)
            {
                return UnprocessableEntity().ToString();
            }
        }

        [HttpGet]
        [Route("/api/estado")]
        public string Estado()
        {
            return "ok";
        }
    }
}