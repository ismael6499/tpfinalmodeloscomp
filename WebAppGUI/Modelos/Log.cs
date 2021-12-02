using System;

namespace WebAppGUI.Modelos
{
    public class Log : Entidad
    {
        public string Componente { get; set; }

        public string Descripcion { get; set; }
        
        public DateTime Fecha { get; set; }

        public string Tipo { get; set; }
    }
}