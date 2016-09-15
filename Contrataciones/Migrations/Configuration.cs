namespace Contrataciones.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Contrataciones.Models.ContextContratacion>
    {
        public Configuration()
        {
            //DUM: habilita las migraciones automaticas.
            AutomaticMigrationsEnabled = true;
            //DUM: indica que en la migracion automatica se pueden perder los datos de una columna si esta se quita o se borra.
            AutomaticMigrationDataLossAllowed = true;
            //DUM: indica el contexto o el modelo donde se gestionan los datos.
            ContextKey = "Contrataciones.Models.ContextContratacion";
        }

        protected override void Seed(Contrataciones.Models.ContextContratacion context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
