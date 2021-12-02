using BrazoAPI.Data;
using BrazoAPI.Modelos;
using Nancy;

namespace BrazoAPI
{
    public class MainModule : NancyModule
    {

        public MainModule()
        {
            Get("/api/status", x =>
            {
                if (!Program.brazoService.Encendido)
                {
                    return "off";
                }
                return "ok";
            });
            
            Get("/api/encender", x =>
            {
                Program.brazoService.Encendido = true;
                var url = Program.configuration["urls:api"].Replace("http://","");
                var mapper = new Mapper();
                mapper.Encender(new Brazo(){Url = url});
                return "encendido";
            });
            
            Get("/api/apagar", x =>
            {
                Program.brazoService.Encendido = false;
                var url = Program.configuration["urls:api"].Replace("http://","");
                var mapper = new Mapper();
                mapper.Apagar(new Brazo(){Url = url});
                return "apagado";
            });
        }
    }
}