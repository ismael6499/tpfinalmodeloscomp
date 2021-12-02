using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CintaAPI.Data;
using CintaAPI.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CintaAPI.Controllers
{
    [ApiController]
    public class CintaController : ControllerBase
    {

        private Cinta mCinta;
        public CintaController(Modelos.Cinta cinta)
        {
            this.mCinta = cinta;
        }


        [HttpPost]
        [Route("/api/ponerbulto")]
        public string ponerbulto([FromBody] object body)
        {
            string jsonString = body.ToString();
            Bulto bulto = JsonConvert.DeserializeObject<Bulto>(jsonString);
            mCinta.ponerBulto(bulto);
            return JsonConvert.SerializeObject(bulto);
        }
    }
}