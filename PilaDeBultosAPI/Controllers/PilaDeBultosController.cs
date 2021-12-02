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
        private readonly PilaDeBultos pilaDeBultos;

        public PilaDeBultosController(PilaDeBultos pilaDeBultos)
        {
            this.pilaDeBultos = pilaDeBultos;
        }

        [HttpPost]
        [Route("/api/agregarbulto")]
        public string AgregarBulto([FromBody] object body)
        {
            string jsonBody = body.ToString();
            try
            {
                Bulto bulto = JsonConvert.DeserializeObject<Bulto>(jsonBody);
                pilaDeBultos.agregarBulto(bulto);
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