using System;
using System.ComponentModel.DataAnnotations;

namespace CintaAPI.Modelos
{
    public class Cinta : Entidad
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Url { get; set; }


        public bool Encendido { get; set; }

    }
}
