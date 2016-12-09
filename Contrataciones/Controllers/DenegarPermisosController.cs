using Contrataciones.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contrataciones.Controllers
{
    public class DenegarPermisosController : Controller
    {
        private ContextContratacion dbContrata = new ContextContratacion();

        // GET: DenegarPermisos
        public ActionResult Index()
        {
            return View(this.ObtenerOpcionesMenu());
        }

        /// <summary>
        /// valida si la opcion se le denego el permiso, si ya esta registrada es porque se le denego el permiso para el rol.
        /// </summary>
        /// <param name="controladorAccion"></param>
        /// <param name="idRol"></param>
        /// <returns>retorna el autonumerico si existe la opcio en los permisos denegado</returns>
        private int ExisteDatosDenegado(string controladorAccion, string idRol = "")
        {
            DenegarPermisos eDenegar = dbContrata.DenegarPermisos.Where(c => c.ControladorAccion.ToLower().Trim() == controladorAccion.ToLower().Trim()).FirstOrDefault();
            if (eDenegar == null)
            {
                return 0;
            }
            else
            {
                return eDenegar.DenegarPermisoID;
            }
        }

        /// <summary>
        /// permite ingresar la opcion a la cual se le denegara el permiso.
        /// </summary>
        /// <param name="denegarPermisoId"></param>
        /// <param name="idRol"></param>
        /// <param name="descripcionMenu"></param>
        /// <param name="controladorAccion"></param>
        /// <param name="permiso"></param>
        /// <returns>retorna el resultado de la transaccion mayor que 0 si es exitosa</returns>
        //[Authorize]
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public JsonResult IngresarPermisoDenegado(string denegarPermisoId, string idRol, string descripcionMenu, string controladorAccion)
        {
            int resultado = 0;
            string controlAccion = controladorAccion.Replace(@"\n", "").Trim();
            denegarPermisoId = HttpUtility.UrlDecode(denegarPermisoId);
            idRol = HttpUtility.UrlDecode(idRol);
            descripcionMenu = HttpUtility.UrlDecode(descripcionMenu);
            controlAccion = HttpUtility.UrlDecode(controladorAccion);
            DenegarPermisos eDenegarPermiso = new DenegarPermisos() { RolId = Convert.ToInt32(idRol), DescripcionMenu = descripcionMenu.Replace(@"\n", "").Trim(), ControladorAccion = controlAccion };

            int idDenegarPermiso = ExisteDatosDenegado(controlAccion);
            if (idDenegarPermiso > 0)
            {
                dbContrata.DenegarPermisos.RemoveRange(dbContrata.DenegarPermisos.Where(c => c.DenegarPermisoID == idDenegarPermiso));
                resultado = dbContrata.SaveChanges();
            }
            else
            {
                dbContrata.DenegarPermisos.Add(eDenegarPermiso);
                resultado = dbContrata.SaveChanges();
            }
            return new JsonResult
            {
                Data = resultado.ToString(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// permite obtener las opciones de menu que tiene configurado el rol del usuario que esta navegando en la aplicacion
        /// </summary>
        /// <returns>retorna la lista de opciones que crean el menu</returns>
        private List<DenegarPermisos> ObtenerOpcionesMenu()
        {
            List<DenegarPermisos> listPermisoDenegado1 = (from me in dbContrata.Menus.ToList()
                                                          select new DenegarPermisos
                                                          {
                                                              DescripcionMenu = me.DescripcionMenu,
                                                              ControladorAccion = me.Controlador + "-" + me.Accion,
                                                              Permiso = Convert.ToBoolean(ExisteDatosDenegado(me.Controlador + "-" + me.Accion))

                                                          }).ToList();

            List<DenegarPermisos> listPermisoDenegado2 = (from me in dbContrata.SubMenuOperaciones.ToList()
                                                          select new DenegarPermisos
                                                          {
                                                              DescripcionMenu = me.DescripcionOperacion,
                                                              ControladorAccion = me.Controlador + "-" + me.Accion,
                                                              Permiso = Convert.ToBoolean(ExisteDatosDenegado(me.Controlador + "-" + me.Accion))
                                                          }).ToList();

            //union all para unificar menu principal y sub menu.
            List<DenegarPermisos> listResul = listPermisoDenegado1.Union(listPermisoDenegado2).ToList();
            return listResul;
        }

        /// <summary>
        /// liberar objeto que se encarga de la conexion con la base de datos.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContrata.Dispose();
                dbContrata = null;
            }

            base.Dispose(disposing);
        }


    }
}
