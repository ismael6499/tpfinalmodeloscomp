using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppGUI.Modelos
{
    public class Bulto : Entidad
    {
        
        [Required]
        public string Descripcion { get; set; } = "Bulto sin descripción";
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string GlobalId { get; set; } = Guid.NewGuid().ToString();
        public string Estado { get; set; } = "Ingresado";
    }
}