using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Contrataciones.Models
{
    public class ContextContratacion : DbContext
    {
        //Comando para habilitar las migraciones de forma automatica
        //Enable-Migrations -ContextTypeName ContextContratacion -EnableAutomaticMigrations
        public ContextContratacion() : base("name=conexionBdContratacion")
        {
        }

        //DUM: Deshabilitar el borrado en cascada
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
      

        public System.Data.Entity.DbSet<Contrataciones.Models.Cargos> Cargos { get; set; }

        public System.Data.Entity.DbSet<Contrataciones.Models.TipoDocumentos> TipoDocumentos { get; set; }

        public System.Data.Entity.DbSet<Contrataciones.Models.Empleados> Empleados { get; set; }

        public System.Data.Entity.DbSet<Contrataciones.Models.Contratos> Contratos { get; set; }

        public System.Data.Entity.DbSet<Contrataciones.Models.Menus> Menus { get; set; }

        public System.Data.Entity.DbSet<Contrataciones.Models.SubMenuOperaciones> SubMenuOperaciones { get; set; }
    }
}
