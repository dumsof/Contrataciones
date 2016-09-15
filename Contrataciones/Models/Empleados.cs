using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrataciones.Models
{
    public class Empleados
    {
        [Key]
        public int IdEmpleado { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

    }
}
