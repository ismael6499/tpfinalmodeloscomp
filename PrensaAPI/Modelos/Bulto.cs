using System;

namespace PrensaAPI.Modelos
{
    public class Bulto
    {
        public int Id { get; set; } = 0;
        public string GlobalId { get; set; }
        public string Descripcion { get; set; } = "Bulto sin descripción";
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Estado { get; set; }
    }
}