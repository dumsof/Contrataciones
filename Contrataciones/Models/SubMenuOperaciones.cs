using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrataciones.Models
{
    public class SubMenuOperaciones
    {
        [Key]
        public int SubMenuOperacionID { get; set; }

        public int MenuID { get; set; }

        public virtual Menus Menus { get; set; }

        [Display(Name = "Es Sub Menú")]        
        public bool EsSubMenu { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string DescripcionOperacion { get; set; }

        [Display(Name = "Controlador")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Controlador { get; set; }

        [Display(Name = "Acción")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Accion { get; set; }

        [Display(Name = "Ordenamiento")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]      
        [Range(1, 99)]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int OrdenamientoSubMenu { get; set; }

    }
}
