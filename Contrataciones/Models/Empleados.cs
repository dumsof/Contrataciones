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

        //inicio relacion para el tipo de documento con el modelo DocumentType
        //relacion de uno a muchos en el cliente
        [Display(Name = "Tipo Documento")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public int TipoDocumentoID { get; set; }

        [Display(Name = "Tipo Documento")]       
        public virtual TipoDocumentos TipoDocumento { get; set; }
        //fin de la relacion entre tipo documento y clientes

        [Display(Name = "Número Documento")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(20, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 6)]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public string NumeroDocumento { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(80, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Nombres { get; set; }

        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(80, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(80, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string SegundoApellido { get; set; }

        public string Direccion { get; set; }

        [Display(Name = "Teléfono Casa")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [StringLength(10, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 7)]
        public string Telefono { get; set; }

        [Display(Name = "Teléfono Celular")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [StringLength(10, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 10)]
        public string Celular { get; set; }

        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(200, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Email { get; set; }


        public virtual ICollection<Contratos> Contratos { get; set; }

    }
}
