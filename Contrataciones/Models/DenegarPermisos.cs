using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contrataciones.Models
{
    public class DenegarPermisos
    {
        [Key]
        public int DenegarPermisoID { get; set; }

        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public int RolId { get; set; }

        [Required(ErrorMessage = "Usted debe ingresar {0}")]        
        [Display(Name = "Descripción Menu")]
        public string DescripcionMenu { get; set; }
       
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [Display(Name = "Controlador Acción")]
        public string ControladorAccion { get; set; }

        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [Display(Name = "Permiso Denegado")]
        public bool Permiso { get; set; }

    }
}