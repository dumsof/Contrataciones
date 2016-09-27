using Contrataciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrataciones.ModelsView
{
    public class MenuVista
    {

        public int MenuID { get; set; }

        public string DescripcionMenu { get; set; }


        public string Controlador { get; set; }


        public string Accion { get; set; }

        public ICollection<SubMenuOperacionesVista> SubMenuOperacion { get; set; }


        public int Ordenamiento { get; set; }
    }
}
