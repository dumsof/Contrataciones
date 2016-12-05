using Contrataciones.Models;
using Contrataciones.ModelsView;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contrataciones.Controllers
{
    public class UsuariosMembershipController : Controller
    {
        private ApplicationDbContext dbSeguridad = new ApplicationDbContext();



        /// <summary>
        /// permite ingresar los datos a la tabla de usuario de meberschip
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns>retorna verdadero si la informacion se ingreso correctamente</returns>
        public bool InsertarUsuarioMemberschip(Empleados empleado)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbSeguridad));
            var rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbSeguridad));
            bool resul = false;
            var user = userManager.FindByName(empleado.Email);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    Email = empleado.Email,
                    UserName = empleado.Email,
                    PasswordHash = empleado.Password
                };
                resul = userManager.Create(user).Succeeded;
            }
            return resul;
        }

        /// <summary>
        /// agrega un nuevo rol a la tabla AspNetRoles
        /// </summary>
        /// <param name="nombreRol"></param>
        /// <returns>retorna true si el rol se agrega con exito</returns>
        public bool AgregarRolMemberschip(string nombreRol)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbSeguridad));
            bool roleResult = false;
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(nombreRol.Trim()))
            {
                roleResult = RoleManager.Create(new IdentityRole(nombreRol.Trim())).Succeeded;
            }
            else
            {
                roleResult = true;
            }
            return roleResult;
        }

        /// <summary>
        /// obtiene todos los roles de memberchi registrados en la tabla  dbo.AspNetRoles
        /// </summary>
        /// <returns>retorna todos los roles registrados</returns>
        public List<RolesVista> ObtenerRoles()
        {
            var rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbSeguridad));
            List<RolesVista> lVistaRol = (from r in rolesManager.Roles.ToList()
                                          select new RolesVista
                                          {
                                              RoleID = r.Id,
                                              Name = r.Name

                                          }).ToList();
            return lVistaRol;
        }

        /// <summary>
        /// permite obtener los roles del usuario que esta navegando actualmente en el sitio.   
        /// </summary>
        /// <returns>retorna el nombre del rol y su id</returns>
        public List<RolesVista> ObtenerRolesUsuarioActual()
        {
            List<RolesVista> listRoles = null;
            if (User != null && !User.Identity.IsAuthenticated)
            {
                string idUsuario = User.Identity.GetUserId();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbSeguridad));
                List<string> rolesForUser = userManager.GetRoles(User.Identity.GetUserId()).ToList();
                listRoles = ObtenerRoles();
                listRoles = listRoles.Where(p => rolesForUser.Contains(p.Name)).ToList();
            }
            return listRoles;
        }
    }
}