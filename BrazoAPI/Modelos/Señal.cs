using System;

namespace BrazoAPI.Modelos
{
    public class Señal
    {
        public Señal(string estado)
        {
            Estado= estado;
        }

        public string Estado { get; set; }
        public DateTime FechaActualizado { get; set; } = DateTime.Now;
    }
}