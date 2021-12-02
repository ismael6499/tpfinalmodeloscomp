using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using BrazoAPI.Data;
using BrazoAPI.Modelos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BrazoAPI
{
    public class BrazoService
    {
        List<string> listaBultosJson = new List<string>();

        private IConnection connection;
        private IConfiguration configuration;


        public void init(IConfiguration configuration)
        {
            this.configuration = configuration;
            string urlRabbit = configuration["urls:rabbit"];

            Logger.GetInstance().WriteLog($"Iniciando Brazo en {urlRabbit}");

            var factory = new ConnectionFactory() { HostName = urlRabbit };

            connection = factory.CreateConnection();
            IModel canalReceiver = connection.CreateModel();

            canalReceiver.QueueDeclare(queue: "Cinta", false, false, false,
                null);

            var consumidor = new EventingBasicConsumer(canalReceiver);
            consumidor.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var bodyString = Encoding.UTF8.GetString(body);

                Logger.GetInstance().WriteLog($"[X] Bulto Recibido en Brazo {bodyString}");
                Bulto bulto = JsonConvert.DeserializeObject<Bulto>(bodyString);
                tomarBulto(bulto);
            };

            canalReceiver.BasicConsume(queue: "Cinta", autoAck: true, consumer: consumidor);

            Console.ReadKey();
        }


        private void tomarBulto(Bulto bulto)
        {
            publicarRecibidoParaCinta(bulto);
            //todo buscar todas las prensas disponibles
            string urlPrensa = configuration["urls:prensa"];
            bool isLibre = checkPrensaLibre(urlPrensa);
            bool isLevantado = checkPrensaLevantada(urlPrensa);
            if (isLibre && isLevantado)
            {
                enviarBultoParaPrensar(bulto,urlPrensa);
                Logger.GetInstance().WriteLog("Se ha enviado el bulto a la prensa");
            }
            else
            {
                Logger.GetInstance().WriteLog("Prensa no disponible");
            }
        }

        private string enviarBultoParaPrensar(Bulto bulto, string urlPrensa)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(urlPrensa);
                    var jsonBulto = JsonConvert.SerializeObject(bulto);
                    StringContent stringContent = new StringContent(jsonBulto, Encoding.UTF8, "application/json");
                    var responseTask = httpClient.PostAsync("/api/prensar",stringContent);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string readResult = readTask.Result;
                    return readResult;
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }

        private bool checkPrensaLibre(string urlPrensa)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(urlPrensa);
                    var responseTask =  httpClient.GetAsync("/api/libre");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string readResult = readTask.Result;
                        var boolean = Boolean.Parse(readResult);
                        return boolean;
                    }
                }
                catch (Exception e)
                {
                }
                return false;
            }
            
        }
        
        private bool checkPrensaLevantada(string urlPrensa)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(urlPrensa);
                    var responseTask =  httpClient.GetAsync("/api/estado");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string readResult = readTask.Result;
                        Señal señal = JsonConvert.DeserializeObject<Señal>(readResult);
                        return señal.Estado == "Levantado";
                    }
                }
                catch (Exception e)
                {
                }
                return false;
            }
            
        }

        private void publicarRecibidoParaCinta(Bulto bulto)
        {
            using (var canalReceiver = connection.CreateModel())
            {
                string cintaQueue = "CintaReceived";
                canalReceiver.QueueDeclare(queue: cintaQueue, false, false, false,
                    null);
                string jsonBulto = JsonConvert.SerializeObject(bulto);
                var bytes = Encoding.UTF8.GetBytes(jsonBulto);
                canalReceiver.BasicPublish("", cintaQueue, false, null, bytes);
                Logger.GetInstance().WriteLog("Quitando elemento de la cinta");
            }
        }
    }
}