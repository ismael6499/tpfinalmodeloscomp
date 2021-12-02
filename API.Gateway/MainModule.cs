using System;
using System.Net.Http;
using System.Text;
using API.Gateway;
using Nancy;
using Nancy.Extensions;

namespace ApiGateway
{
    public class MainModule : NancyModule
    {

        public MainModule()
        {

            string urlCinta = Program.Configuration.GetSection("Cinta").GetSection("url").Value;
            string urlBrazo = Program.Configuration.GetSection("Brazo").GetSection("url").Value;
            string urlPrensa = Program.Configuration.GetSection("Prensa").GetSection("url").Value;
            string urlSensores = Program.Configuration.GetSection("Sensores").GetSection("url").Value;
            
            
            Post("/v1/ponerbulto", _ =>
            {
                var bultoJson = Context.Request.Body.AsString();
                
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        httpClient.BaseAddress = new Uri(urlCinta);
                        StringContent stringContent = new StringContent(bultoJson,Encoding.UTF8,"application/json");
                        var responseTask =  httpClient.PostAsync("/api/ponerbulto",stringContent);
                        responseTask.Wait();
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();
                            var readResult = readTask.Result;
                            return readResult;
                        }

                        var readAsStringAsync = result.Content.ReadAsStringAsync();
                        readAsStringAsync.Wait();
                        return readAsStringAsync.Result;
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }

                    return  "Error al consultar";
                }
            });
            
        }
    }
}