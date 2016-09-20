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

        //relacion de uno a muchos en el cliente
        [Display(Name = "Tipo Documento")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public int IdEmpleado { get; set; }

        [Display(Name = "Empleado")]
        public virtual Empleados Empleado { get; set; }
        //fin de la relacion entre tipo documento y clientes

        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [Display(Name = "Disponibilidad Presupuestal CDP")]
        public string DisponibilidadPresupuestalCDP { get; set; }


        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [Display(Name = "Compromiso Presupuestal RP")]
        public string CompromisoPresupuestalRP { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Inicial")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Final")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public DateTime FechaFinal { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Valor Contrato")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public decimal ValorContrato { get; set; }

        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [Display(Name = "Cargo")]
        public virtual ICollection<Cargos> Cargos { get; set; }
    }
}
