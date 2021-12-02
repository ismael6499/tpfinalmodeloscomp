using System;
using System.Collections.Generic;
using System.Text;
using CintaAPI.Data;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CintaAPI.Modelos
{
    public class Cinta
    {
        private Dictionary<string, Bulto> bultosDict = new();
        
        private readonly ConnectionFactory factory;
        private readonly IConnection connection;
        private readonly IModel canalCintaPublish;
        private readonly IModel canalCintaReceived;
        private readonly EventingBasicConsumer consumer;

        public Cinta()
        {
            string urlRabbit = Program.configuration["urls:rabbit"];
            factory = new ConnectionFactory() { HostName = urlRabbit };
            connection = factory.CreateConnection();
            canalCintaPublish = connection.CreateModel();
            canalCintaPublish.QueueDeclare(queue: "Cinta", durable: false, exclusive: false, autoDelete: false,
                arguments: null);
            
            canalCintaReceived = connection.CreateModel();
            canalCintaReceived.QueueDeclare(queue: "CintaReceived", durable: false, exclusive: false, autoDelete: false,
                arguments: null);
            consumer = new EventingBasicConsumer(canalCintaReceived);
            consumer.Received +=  (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var bodyString = Encoding.UTF8.GetString(body);
                sacarBulto(bodyString);
            };
            canalCintaReceived.BasicConsume("CintaReceived", true, "", null, consumer);
        }

        private void sacarBulto(string bodyString)
        {
            Bulto bulto = JsonConvert.DeserializeObject<Bulto>(bodyString);
            bultosDict.Remove(bulto.GlobalId);
            Logger.GetInstance().WriteLog($"Bulto {bulto.GlobalId} quitado de la cinta");
        }

        public void ponerBulto(Bulto bulto)
        {
            Logger.GetInstance().WriteLog($"Bulto {bulto.GlobalId} puesto en la cinta");
            bultosDict.Add(bulto.GlobalId, bulto);
            publicarEnRabbit(bulto);
        }

        private void publicarEnRabbit(Bulto bulto)
        {
            string bultoJson = JsonConvert.SerializeObject(bulto);
            var body = Encoding.UTF8.GetBytes(bultoJson);
            canalCintaPublish.BasicPublish("", routingKey: "Cinta", basicProperties: null, body: body);
            Logger.GetInstance().WriteLog($"Avisando a brazo de bulto disponible: {bultoJson}");
        }
        
        
    }
}