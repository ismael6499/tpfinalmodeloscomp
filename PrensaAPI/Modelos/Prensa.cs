using System;
using System.Diagnostics.SymbolStore;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetMQ;
using NetMQ.Sockets;
using PrensaAPI.Data;

namespace PrensaAPI.Modelos
{
    public class Prensa
    {
        public string Estado { get; set; } = EstadoPrensaConstants.LEVANTADO;

        public async Task<Bulto> Prensar(Bulto bulto)
        {
            Estado = EstadoPrensaConstants.PRENSANDO;
            ConsultaZmq($"$set${Estado}");
            Logger.GetInstance().WriteLog("Prensando bulto " + bulto.GlobalId);
            await Task.Delay(2000);
            Logger.GetInstance().WriteLog("Levantando...");
            Estado = EstadoPrensaConstants.LEVANTADO;
            ConsultaZmq($"$set${Estado}");
            bulto.FechaHoraPrensado = DateTime.Now;
            return bulto;
        }

        public bool callbackVerificarLevantado()
        {
            if (Estado == EstadoPrensaConstants.LEVANTADO)
            {
                return true;
            }

            return false;
        }

        public string verificarEstado(string requestString)
        {
            return ConsultaZmq(requestString);
        }

        private string ConsultaZmq(string requestString)
        {
            string urlSensorActivo = Program.configuration["urls:sensorActivo"];
            int puertoNro = 11112;
            string urlIp = "";

            if (urlSensorActivo != null)
            {
                try
                {
                    var urlCompuesto = urlSensorActivo.Split(":");
                    string urlPuerto = "";
                    if (urlCompuesto[0] != "")
                    {
                        urlIp = urlCompuesto[0];
                    }

                    if (urlCompuesto[1] != "")
                    {
                        urlPuerto = urlCompuesto[1];
                    }

                    puertoNro = int.Parse(urlPuerto);
                }
                catch (Exception e)
                {
                    Logger.GetInstance().WriteLog("Error al intentar parsear IP sensor Activo");
                    Logger.GetInstance().WriteLogError(e.ToString());
                }
            }

            using (var client = new RequestSocket($"tcp://{urlIp}:{puertoNro}"))
            {
                client.SendFrame(requestString);

                string dataRecibida = client.ReceiveFrameString();
                if (dataRecibida.Contains("executeCallback"))
                {
                    bool valor = callbackVerificarLevantado();
                    client.SendFrame(valor.ToString());
                    string valorFinalRecibido = client.ReceiveFrameString();
                    return valorFinalRecibido;
                }

                return dataRecibida;
            }
        }

        public bool consultarSensorLibre()
        {
            string urlSensorPasivo = Program.configuration["urls:sensorPasivo"];
            int puertoNro = 11111;
            string mensajeRecibido = "";
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];

                if (urlSensorPasivo != null)
                {
                    try
                    {
                        var urlCompuesto = urlSensorPasivo.Split(":");
                        string urlIp = "";
                        string urlPuerto = "";
                        if (urlCompuesto[0] != "")
                        {
                            urlIp = urlCompuesto[0];
                        }

                        if (urlCompuesto[1] != "")
                        {
                            urlPuerto = urlCompuesto[1];
                        }

                        ipAddr = IPAddress.Parse(urlIp);
                        puertoNro = int.Parse(urlPuerto);
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance().WriteLog("Error al intentar parsear IP sensor pasivo");
                        Logger.GetInstance().WriteLogError(e.ToString());
                    }
                }

                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, puertoNro);
                Socket sender = new Socket(ipAddr.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(localEndPoint);

                try
                {
                    Logger.GetInstance().WriteLog($"Conectado a IP:  -> {sender.RemoteEndPoint} ");
                }
                catch (Exception e)
                {
                    // ignored
                }

                byte[] messageSent = Encoding.ASCII.GetBytes("$get$<EOT>");
                int byteSent = sender.Send(messageSent);

                byte[] messageReceived = new byte[1024 * 2];

                int byteRecv = sender.Receive(messageReceived);

                mensajeRecibido = Encoding.ASCII.GetString(messageReceived,
                    0, byteRecv);

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception e)
            {
                Logger.GetInstance().WriteLogError(e.ToString());
            }

            bool valorRecibido = bool.Parse(mensajeRecibido);
            return valorRecibido;
        }
    }

    public static class EstadoPrensaConstants
    {
        public const string PRENSANDO = "Prensando";
        public const string LEVANTADO = "Levantado";
    }
}