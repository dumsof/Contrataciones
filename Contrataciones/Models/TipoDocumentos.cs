using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrataciones.Models
{
   public class TipoDocumentos
    {
        [Key]
        public int TipoDocumentoID { get; set; }

        [Display(Name = "Tipo Documento")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 5)]
        [RegularExpression(@"^([a-zA-ZñÑáéíóúÁÉÍÓÚ0-9 \.\&\'\-]+)$", ErrorMessage = "El campo {0} no debe tener caracteres especiales, verifique.")]
        public string DescriptionDocumento { get; set; }

        public virtual ICollection<Empleados> Empleados { get; set; }
}
}
