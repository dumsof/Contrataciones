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
            //poder obtener los usuarios de las tablas memberchid
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbSeguridad));
            //se obtiene la informacion de la tabla usuarios y se pasa al modelo de vista para poder pintar en la pagina.
            //List<UsuarioVista> lvistaUsuario = (from u in userManager.Users.ToList()
            //                                    select new UsuarioVista
            //                                    {
            //                                        UserID = u.Id,
            //                                        Name = ccn.Empleados.Where(c => c.Email.ToLower().Trim() == u.Email.ToLower().Trim()).Select(s => s.Nombres + " " + s.PrimerApellido + " " + s.SegundoApellido).FirstOrDefault(),
            //                                        Email = u.Email
            //                                    }).ToList();

            //realizar un join para que sea mas rapido lo de arriba.
            List<UsuarioVista> lvistaUsuario = (from U in userManager.Users.ToList()
                                                join E in ccn.Empleados.ToList() on U.Email.ToLower().Trim() equals E.Email.ToLower().Trim()
                                                select new UsuarioVista
                                                {
                                                    UserID = U.Id,
                                                    Name = E.Nombres + " " + E.PrimerApellido + " " + E.SegundoApellido + " - " + E.NumeroDocumento,
                                                    Email = U.Email
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
            //var user = userManager.FindById(idUsuario);

            if (userManager.IsInRole(idUsuario, nombreRolSeleccionado))
            {
                //remover asignacion de la tbla AspNetUserRoles
                var remFromRole = userManager.RemoveFromRole(idUsuario, nombreRolSeleccionado);
            }
            else
            {
                //agregar registro en la tabla AspNetUserRoles, si el usuario no tiene asignado el rol.
                var respuesta = userManager.AddToRole(idUsuario, nombreRolSeleccionado);

            }

            return Json(new { value = "new" });
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