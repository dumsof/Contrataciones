using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrataciones.ModelsView
{
    public class RolesVista
    {
        public bool RolAsignado { get; set; }
        public string RoleID { get; set; }

        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [Display(Name = "Descripción")]        
        [StringLength(100, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 5)]
        public string Name { get; set; }
    }
}
