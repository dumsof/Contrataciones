using Contrataciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Contrataciones.Permisos
{
    public class PermisoAttribute : ActionFilterAttribute
    {
        public string Permiso { get; set; }
        private ContextContratacion db = new ContextContratacion();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            bool condicionNoTienePermiso = false;
            DenegarPermisos eDenegar = db.DenegarPermisos.Where(c => c.ControladorAccion.ToLower().Trim() == this.Permiso.ToLower().Trim()).FirstOrDefault();
            if (eDenegar != null)
            {
                condicionNoTienePermiso = true;
            }
            //si la opcion esta registrada en denegar permisos, es porque el usuario esta asignado en un rol que no permite entrar 
            //a esta opción
            if (condicionNoTienePermiso)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }
    }
}
