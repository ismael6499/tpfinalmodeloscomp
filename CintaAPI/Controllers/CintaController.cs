using CintaAPI.Data;
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


        [HttpGet]
        [Route("/api/encender")]
        public string Encender()
        {
            _mCintaService.Encender();
            var url = this.Request.Host.ToString();
            var mapper = new Mapper();
            mapper.Encender(new Cinta(){Url = url});
            return "Encendido";
        }
        
        
        [HttpGet]
        [Route("/api/apagar")]
        public string Apagar()
        {
            _mCintaService.Apagar();
            var url = this.Request.Host.ToString();
            var mapper = new Mapper();
            mapper.Apagar(new Cinta(){Url = url});
            return "Encendido";
        }
        
        
        [HttpPost]
        [Route("/api/ponerbulto")]
        public string ponerbulto([FromBody] object body)
        {
            if(!_mCintaService.Encendido)
            {
                return "La cinta no está encendida";
            }
            string jsonString = body.ToString();
            Bulto bulto = JsonConvert.DeserializeObject<Bulto>(jsonString);
            bulto.Estado = "Iniciado";
            _mCintaService.ponerBulto(bulto);
            Logger.GetInstance().SaveBultoLog(bulto);
            return JsonConvert.SerializeObject(bulto);
        }
    }
}