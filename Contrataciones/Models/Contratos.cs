using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrataciones.Models
{
    public class Contratos
    {
        [Key]
        public int ContratoID { get; set; }

        [Required]
        [Display(Name = "Correo electrónico")]
        public string DescripcionContrato { get; set; }
    }
}
