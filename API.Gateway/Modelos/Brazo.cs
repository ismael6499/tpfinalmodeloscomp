using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Gateway.Modelos;

namespace API.Gateway.Modelos
{
    public class Brazo : Entidad
    {
        [Required]
        public string Nombre { get; set; }
        
        [Required]
        public string Url { get; set; }


        public bool Encendido { get; set; }



    }
}
