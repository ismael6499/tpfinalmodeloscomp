using System.Collections.Generic;
using PilaDeBultosAPI.Data;

namespace PilaDeBultosAPI
{
    public class PilaDeBultos
    {
        public List<Bulto> Bultos { get; } = new List<Bulto>();

        public void agregarBulto(Bulto bulto)
        {
            Bultos.Add(bulto);
            Logger.GetInstance().WriteLog($"Bulto {bulto.GlobalId} agregado a la pila de bultos");
        }

        
    }
}