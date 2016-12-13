using Contrataciones.Controllers.Utilidades;
using Contrataciones.Models;
using Contrataciones.ModelsView;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Contrataciones.Controllers
{
    public class MenuDinamicoController : Controller
    {
        private ContextContratacion cnx = new ContextContratacion();
        // GET: MenuDinamico
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult UsuarioAutentificado()
        {
            int estaAutentificado = 0;
            if (User.Identity.IsAuthenticated)
            {
                estaAutentificado = 1;
            }
            return new JsonResult
            {
                Data = estaAutentificado,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult ObtenerItemMenu()
        {
            //validar que solo se pueda generar el menu si el usuario esta autentificado
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult
                {
                    Data = null,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            string url = Request.Url.ToString();
            //Inicio mostrar solo los item que no estan en los permisos denegados.
            List<string> listRoles = ObtenerRolesUsuario().Select(s => s.RoleID).ToList();
            List<DenegarPermisos> listDenegarPermiso = (from d in cnx.DenegarPermisos
                                                        where listRoles.Contains(d.RolId.ToString())
                                                        select d).ToList();
            List<string> listPermisoDenegado = listDenegarPermiso.Select(s => s.ControladorAccion).ToList();
            //Fin buscar item permiso denegado.

            //se crean modelos auxiliares para solucionar el error de referencia circular.
            //que se produce cuando dos modelos estan relacionados.
            List<MenuVista> listMenu = (from m in cnx.Menus
                                        where !listPermisoDenegado.Contains((m.Controlador + "-" + m.Accion).Trim())
                                        select new MenuVista
                                        {
                                            MenuID = m.MenuID,
                                            DescripcionMenu = m.DescripcionMenu,
                                            Controlador = m.Controlador,
                                            Accion = m.Accion,
                                            Url = url,
                                            SubMenuOperacion = (from subM in m.SubMenuOperaciones.Where(c => c.EsSubMenu)
                                                                where !listPermisoDenegado.Contains((subM.Controlador + "-" + subM.Accion).Trim())
                                                                select new SubMenuOperacionesVista
                                                                {
                                                                    DescripcionOperacion = subM.DescripcionOperacion,
                                                                    Accion = subM.Accion,
                                                                    Controlador = subM.Controlador
                                                                }).ToList()

                                        }).ToList();

            return new JsonResult
            {
                Data = listMenu.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// obtener el rol o los roles a los cuales esta asignado el usuario que se logueo en el sitio.
        /// </summary>
        /// <returns>retorna el rol o roles el cual esta logueado el usuario</returns>
        public List<RolesVista> ObtenerRolesUsuario()
        {
            List<RolesVista> listRoles = new List<RolesVista>();
            if (SessionHelper.GetSession<RolesVista>(SessionKey.ROLES_USUARIO) == null)
            {
                UsuariosMembershipController objUsua = new UsuariosMembershipController();
                if (User != null && User.Identity.IsAuthenticated)
                {
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    List<string> rolesForUser = userManager.GetRoles(User.Identity.GetUserId()).ToList();
                    listRoles = objUsua.ObtenerRoles();
                    listRoles = listRoles.Where(p => rolesForUser.Contains(p.Name)).ToList();
                    SessionHelper.SetSession(SessionKey.ROLES_USUARIO, listRoles);
                }
            }
            return listRoles;
        }

        /// <summary>
        /// metodo para obtener el controlador y accion al cual se le nego el permiso.
        /// </summary>
        /// <returns>retorna concatenado la accion y permiso denegado</returns>
        private List<string> ObtenerPermisoDenegado()
        {
            List<string> listPermisoDenegado = new List<string>();
            List<string> listRoles = null;
            if (listRoles != null && listRoles.Count > 0)
            {
                listRoles = ObtenerRolesUsuario().Select(s => s.RoleID).ToList();
            }
            List<DenegarPermisos> listDenegarPermiso = (from d in cnx.DenegarPermisos
                                                        where listRoles.Contains(d.RolId.ToString())
                                                        select d).ToList();
            if (listDenegarPermiso != null && listDenegarPermiso.Count > 0)
            {
                listPermisoDenegado = listDenegarPermiso.Select(s => s.ControladorAccion).ToList();
            }
            return listPermisoDenegado;
        }

    }
}