using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using SensorActivo.Data;
using SensorActivo.Models;

namespace SensorActivo
{
    class Program
    {
        private static Señal estadoActual = new("Levantado");

        public static IConfiguration configuration;

        static void Main(string[] args)
        {
            configuration = CreateConfiguration(args);

            // Wire up the CTRL+C handler
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Cerrando sistema...");
                Environment.Exit(0);
            };
            Console.WriteLine("Presione Ctrl + C para cerrar el sistema");
            ExecuteServer(configuration);
        }

        public static void ExecuteServer(IConfiguration configuration)
        {
            string puerto = configuration["url:puerto"];
            string ip = configuration["url:ip"];
            if (ip != null)
            {
                ip = "localhost";
            }

            int puertoNro = 11112;
            if (puerto != null)
            {
                try
                {
                    puertoNro = int.Parse(puerto);
                }
                catch (Exception ex)
                {
                    Logger.GetInstance()
                        .WriteLogError("Error al intentar convertir el puerto. Puerto por defecto 11112 aplicado. " +
                                  ex.ToString());
                }
            }

            try
            {
                using (var server = new ResponseSocket($"tcp://{ip}:{puertoNro}"))
                {
                    Logger.GetInstance().WriteLog($"Iniciando sensor pasivo en tcp://{ip}:{puertoNro}");
                    while (true)
                    {
                        Logger.GetInstance().WriteLog("Esperando Conexiones ... ");

                        string data = server.ReceiveFrameString();

                        String mensaje = "";
                        if (data.Contains("$get$"))
                        {
                            mensaje = JsonConvert.SerializeObject(estadoActual);
                            Logger.GetInstance().WriteLog("Get Procesado");
                        }
                        else if (data.Contains("$levantado$"))
                        {
                            server.SendFrame("executeCallback");
                            Logger.GetInstance().WriteLog("Ejecutando Callback");
                            string valorRecibido = server.ReceiveFrameString();

                            mensaje = validarValorRecibido(valorRecibido);
                            Logger.GetInstance().WriteLog("Levantado Procesado");
                        }
                        else if (data.Contains("$set$"))
                        {
                            string valorToSet = data.Replace("$set$", "");

                            mensaje = validarValorRecibido(valorToSet);

                            Logger.GetInstance().WriteLog("Set procesado");
                        }

                        server.SendFrame(mensaje);
                        Logger.GetInstance().WriteLog("Enviando respuesta " + mensaje);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance().WriteLogError(e.ToString());
            }
        }

        private static string validarValorRecibido(string valorRecibido)
        {
            string mensaje;
            try
            {
                bool valorNuevo = bool.Parse(valorRecibido);
                if (valorNuevo)
                {
                    estadoActual.Estado = "Levantado";
                }
                else
                {
                    estadoActual.Estado = "Bajado";
                }

                estadoActual.FechaActualizado = DateTime.Now;
                mensaje = JsonConvert.SerializeObject(estadoActual);
            }
            catch (Exception)
            {
                mensaje = "Error al settear valor";
            }

            return mensaje;
        }

        private static IConfiguration CreateConfiguration(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            return configuration;
        }
    }
}