using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contrataciones.ModelsView
{
    public class UsuarioVista
    {
        public string UserID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public List<RolesVista> Roles { get; set; }
    }
}
