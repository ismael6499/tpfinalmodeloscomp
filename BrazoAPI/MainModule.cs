using Nancy;

namespace BrazoAPI
{
    public class MainModule : NancyModule
    {

        public MainModule()
        {
            Get("/api/status", x =>
            {
                return "ok";
            });
            
        }
    }
}