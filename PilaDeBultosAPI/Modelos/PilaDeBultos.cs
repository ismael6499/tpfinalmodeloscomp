using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PilaDeBultosAPI.Modelos
{
    public class PilaDeBultos : Entidad
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Url { get; set; }

        public bool Conectado { get; set; }


        public List<Bulto> Bultos { get; set; }

    }
}
