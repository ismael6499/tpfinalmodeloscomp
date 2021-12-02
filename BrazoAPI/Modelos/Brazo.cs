using System;
using Microsoft.Build.Framework;

namespace BrazoAPI.Modelos
{
    public class Brazo : Entidad
    {
        [Required]
        public string Nombre { get; set; }
        
        [Required]
        public string Url { get; set; }

        public bool Conectado { get; set; }

        public bool Encendido { get; set; }



    }
}