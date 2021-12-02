using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppGUI.Modelos
{
    public class Bulto : Entidad
    {
        [Required]
        public string Descripcion { get; set; } = "Bulto sin descripción";
        public DateTime Fecha { get; set; } = DateTime.Now;
        public DateTime FechaHoraPrensado { get; set; } 
        public string GlobalId { get; set; } = Guid.NewGuid().ToString();

    }
}