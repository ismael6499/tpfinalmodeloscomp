using System;
using BrazoAPI.Modelos;

namespace BrazoAPI
{
    public class Bulto : Entidad
    {
        public string Descripcion { get; set; } = "Bulto sin descripción";
        public string GlobalId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Estado { get; set; }
    }
}