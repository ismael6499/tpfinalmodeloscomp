using CintaAPI.Modelos;
using CintaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CintaAPI.Controllers
{
    [ApiController]
    public class CintaController : ControllerBase
    {

        private CintaService _mCintaService;
        public CintaController(CintaService cintaService)
        {
            this._mCintaService = cintaService;
        }


        [HttpPost]
        [Route("/api/ponerbulto")]
        public string ponerbulto([FromBody] object body)
        {
            string jsonString = body.ToString();
            Bulto bulto = JsonConvert.DeserializeObject<Bulto>(jsonString);
            _mCintaService.ponerBulto(bulto);
            return JsonConvert.SerializeObject(bulto);
        }
    }
}