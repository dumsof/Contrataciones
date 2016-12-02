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
    public class RolesController : Controller
    {
        private ApplicationDbContext dbSeguridad = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            return View(ObtenerObjeto().ObtenerRoles());
        }


        // GET: Empleados/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleID,Name")] RolesVista rol)
        {
            if (ModelState.IsValid)
            {
                ObtenerObjeto().AgregarRolMemberschip(rol.Name);
                return RedirectToAction("Index");
            }

            //ViewBag.TipoDocumentoID = new SelectList(db.TipoDocumentos, "TipoDocumentoID", "DescriptionDocumento", empleados.TipoDocumentoID);
            return View(rol);
        }

        public ActionResult RolesDenegarPermiso(string userID)
        {

            return View();
        }

        /// <summary>
        /// permite crear una instancia del objeto de la clase que permite la administracion de los roles
        /// </summary>
        /// <returns>retorna un objeto instanciado</returns>
        private UsuariosMembershipController ObtenerObjeto()
        {
            return new UsuariosMembershipController();
        }
    }
}