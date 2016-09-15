using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrataciones.Controllers.Utilidades;

namespace Contrataciones.Models
{
    public class Cargos
    {
        [Key]
        public int CargoID { get; set; }


        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string DescripcionCargo { get; set; }


    }
}
