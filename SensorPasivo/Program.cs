using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Configuration;
using Sensor.Pasivo.Data;

namespace Sensor.Pasivo
{
    class Program
    {
        static bool estadoLibre = true;
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

        public static IPAddress GetIP4Address()
        {
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    return IPA;
                }
            }

            return null;
        }

        public static string ipAdressToString(IPAddress ipAddress)
        {
            string cadena = "";
            Byte[] bytes = ipAddress.GetAddressBytes();
            foreach (byte b in bytes)
            {
                cadena += b + ".";
            }

            cadena = cadena.TrimEnd(".".ToCharArray());
            return cadena;
        }

        public static void ExecuteServer(IConfiguration configuration)
        {
            IPAddress ipAddr = GetIP4Address();
            string puerto = configuration["url:puerto"];
            string ip = configuration["url:ip"];
            if (ip != null)
            {
                try
                {
                    ipAddr = IPAddress.Parse(ip);
                }
                catch (Exception e)
                {
                    Logger.GetInstance()
                        .WriteLogError(
                            $"Error al intentar convertir la ip. IP asignada por defecto  {ipAdressToString(ipAddr)}. {e}");
                }
            }

            int puertoNro = 11111;
            if (puerto != null)
            {
                try
                {
                    puertoNro = int.Parse(puerto);
                }
                catch (Exception ex)
                {
                    Logger.GetInstance()
                        .WriteLogError(
                            $"Error al intentar convertir el puerto. Puerto por defecto 11111 aplicado. {ex}");
                }
            }

            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, puertoNro);

            Socket listener = new Socket(ipAddr.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            Logger.GetInstance().WriteLog("Iniciando sensor pasivo en " + ipAdressToString(ipAddr) + ":" + puertoNro);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(12);

                while (true)
                {
                    Logger.GetInstance().WriteLog("Esperando Conexiones ... ");

                    Socket clientSocket = listener.Accept();

                    byte[] bytes = new Byte[1024 * 2];
                    string data = null;

                    var dateTimeInicio = DateTime.Now;
                    while (true)
                    {
                        int numByte = clientSocket.Receive(bytes);

                        data += Encoding.ASCII.GetString(bytes,
                            0, numByte);

                        if (data.IndexOf("<EOT>", StringComparison.Ordinal) > -1)
                        {
                            data = data.Replace("<EOT>", "");
                            break;
                        }

                        var dateTimeActual = DateTime.Now;
                        TimeSpan diferenciaTiempo = dateTimeActual - dateTimeInicio;
                        if (diferenciaTiempo.Seconds > 60)
                        {
                            break;
                        }
                    }

                    Logger.GetInstance().WriteLog($"Mensaje recibido -> {data} ");
                    String mensaje = "";
                    if (data.Contains("$get$"))
                    {
                        mensaje = estadoLibre.ToString();
                        Logger.GetInstance().WriteLog(mensaje);
                    }
                    else if (data.Contains("$set$"))
                    {
                        string value = data.Replace("$set$", "");
                        try
                        {
                            bool valorNuevo = bool.Parse(value);
                            estadoLibre = valorNuevo;
                            mensaje = "Establecido correctamente";
                            Logger.GetInstance().WriteLog(mensaje);
                        }
                        catch (Exception e)
                        {
                            mensaje = "Error al intentar establecer el nuevo valor";
                            Logger.GetInstance().WriteLogError(mensaje);
                        }
                    }

                    byte[] message = Encoding.ASCII.GetBytes(mensaje);

                    clientSocket.Send(message);

                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }

            catch (Exception e)
            {
                Logger.GetInstance().WriteLogError(e.ToString());
            }
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