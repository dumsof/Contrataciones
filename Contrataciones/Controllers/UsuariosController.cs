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
    public class UsuariosController : Controller
    {
        private ApplicationDbContext dbSeguridad = new ApplicationDbContext();
        private ContextContratacion ccn = new ContextContratacion();
        // GET: Usuarios
        public ActionResult Index()
        {
            //obtener el parametro de url idrol para mostrar los usuarios seleccionados.
            string rolID = Convert.ToString(RouteData.Values["idRol"]);
            
            //poder obtener los usuarios de las tablas memberchid
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbSeguridad));

            //realizar un join para que sea mas rapido lo de arriba.
            List<UsuarioVista> lvistaUsuario = (from U in userManager.Users.ToList()
                                                join E in ccn.Empleados.ToList() on U.Email.ToLower().Trim() equals E.Email.ToLower().Trim()
                                                select new UsuarioVista
                                                {
                                                    UserID = U.Id,
                                                    Name = E.Nombres + " " + E.PrimerApellido + " " + E.SegundoApellido + " - " + E.NumeroDocumento,
                                                    Email = U.Email,
                                                    Asignado = ObtenerUsuarioAsignadoRol(U.Id, rolID),

                                                }).ToList();
            return View(lvistaUsuario);
        }

        public ActionResult Roles(string userID)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbSeguridad));
            ApplicationUser user = userManager.Users.ToList().Find(u => u.Id == userID);
            var rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbSeguridad));
            //se obtienen los 
            List<RolesVista> lVistaRol = (from r in rolesManager.Roles.ToList()
                                          select new RolesVista
                                          {
                                              RolAsignado = ObtenerRolAsignadoUsuario(user, r.Id),
                                              RoleID = r.Id,
                                              Name = r.Name

                                          }).ToList();

            UsuarioVista pVistaUsuario = new UsuarioVista()
            {
                UserID = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Roles = lVistaRol
            };

            return View(pVistaUsuario);
        }

        //[HttpPost]
        public ActionResult AgregarRol(string idRol, string idUsuario)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbSeguridad));
            var rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbSeguridad));
            idRol = HttpUtility.UrlDecode(idRol);            //Server.UrlDecode(
            string nombreRolSeleccionado = rolesManager.Roles.ToList().Find(c => c.Id == idRol).Name;
            bool resulTran = false;
            //var user = userManager.FindById(idUsuario);
            int proceso = 0;
            if (userManager.IsInRole(idUsuario, nombreRolSeleccionado))
            {
                //remover asignacion de la tbla AspNetUserRoles
                proceso = 1;
                resulTran = userManager.RemoveFromRole(idUsuario, nombreRolSeleccionado).Succeeded;
            }
            else
            {
                proceso = 2;
                //agregar registro en la tabla AspNetUserRoles, si el usuario no tiene asignado el rol.
                resulTran = userManager.AddToRole(idUsuario, nombreRolSeleccionado).Succeeded;

            }
            return Json(new { respuesta = resulTran, tipoProceso = proceso });
        }

        private bool ObtenerRolAsignadoUsuario(ApplicationUser user, string RoleID)
        {
            var rol = user.Roles.ToList().Find(b => b.RoleId == RoleID);
            if (rol != null)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// permite marcar el check si el usuario ya esta asignado al rol al cual se quiere agregar
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="RoleID"></param>
        /// <returns>retorna verdadero o true si el usuairo tiene el rol asignado</returns>

        private bool ObtenerUsuarioAsignadoRol(string userID, string RoleID)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbSeguridad));
            ApplicationUser user = userManager.Users.ToList().Find(u => u.Id == userID);
            var rol = user.Roles.ToList().Find(b => b.RoleId == RoleID);
            if (rol != null)
            {
                return true;
            }
            return false;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbSeguridad.Dispose();
                dbSeguridad = null;
            }

            base.Dispose(disposing);
        }
    }
}