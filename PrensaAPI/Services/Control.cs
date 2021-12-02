using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrensaAPI.Data;

namespace PrensaAPI.Modelos
{
    public class Control : IObservable
    {
        private HashSet<string> listaUrlsObservadores = new();

        public Control()
        {
            string[] urlsPilasDeBultos = Program.configuration.GetSection("urls:pilasDeBultos").GetChildren().ToArray()
                .Select(c => c.Value).ToArray();
            if (urlsPilasDeBultos.Length > 0)
            {
                foreach (var url in urlsPilasDeBultos)
                {
                    addObserver(url);
                }
                Logger.GetInstance().WriteLog("Control",$"Añadidos observers al control");
            }
        }

        public void addObserver(string url)
        {
            listaUrlsObservadores.Add(url);
        }

        public void removeObserver(string url)
        {
            listaUrlsObservadores.Remove(url);
        }

        public string llevarBultoALaPila(Bulto bulto)
        {
            bool procesado = false;
            foreach (var urlPilaBultos in listaUrlsObservadores)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        Logger.GetInstance().WriteLog("Control",$"Intentando conectar con {urlPilaBultos}");
                        httpClient.BaseAddress = new Uri(urlPilaBultos);
                        Task<string> task = httpClient.GetStringAsync("/api/estado");
                        task.Wait();
                        string resultGet = task.Result;
                        if (resultGet.ToLower() == "ok")
                        {
                            Logger.GetInstance().WriteLog("Control",$"Conectado");
                            var jsonBulto = JsonConvert.SerializeObject(bulto);
                            StringContent stringContent = new StringContent(jsonBulto, Encoding.UTF8, "application/json");
                            var responseTask = httpClient.PostAsync("/api/agregarbulto", stringContent);
                            responseTask.Wait();
                            var result = responseTask.Result;
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();
                            string readResult = readTask.Result;
                            if (readResult.ToLower().Equals("ok"))
                            {
                                Logger.GetInstance().WriteLog("Control",$"Procesado en {urlPilaBultos}");
                                procesado = true;
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
            string msjAgregadoPila = "No se pudo llevar a la pila de bultos";
            if(procesado)
            {
                msjAgregadoPila = "Agregado a la pila";
            }
            return msjAgregadoPila;
        }
    }
}