using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppGUI.Modelos
{
    public class SensorActivo : Entidad
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Url { get; set; }


        public string Estado { get; set; }

    }
}
