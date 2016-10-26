using Contrataciones.Models;
using Contrataciones.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contrataciones.Controllers
{
    public class MenuDinamicoController : Controller
    {
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
            ContextContratacion cnx = new ContextContratacion();
            string url = Request.Url.ToString();

            //se crean modelos auxiliares para solucionar el error de referencia circular.
            //que se produce cuando dos modelos estan relacionados.
            var listMenu = from m in cnx.Menus
                           select new MenuVista
                           {
                               MenuID = m.MenuID,
                               DescripcionMenu = m.DescripcionMenu,
                               Controlador = m.Controlador,
                               Accion = m.Accion,
                               Url = url,
                               SubMenuOperacion = (from subM in m.SubMenuOperaciones.Where(c => c.EsSubMenu)
                                                   select new SubMenuOperacionesVista
                                                   {
                                                       DescripcionOperacion = subM.DescripcionOperacion,
                                                       Accion = subM.Accion,
                                                       Controlador = subM.Controlador
                                                   }).ToList()

                           };



            return new JsonResult
            {
                Data = listMenu.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
    }
}