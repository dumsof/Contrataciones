using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Contrataciones.Models;

namespace Contrataciones.Controllers
{
    public class EmpleadosController : Controller
    {
        private ContextContratacion db = new ContextContratacion();

        // GET: Empleados
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.TipoDocumento);
            return View(empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.TipoDocumentoID = new SelectList(db.TipoDocumentos, "TipoDocumentoID", "DescriptionDocumento");
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEmpleado,TipoDocumentoID,NumeroDocumento,Nombres,PrimerApellido,SegundoApellido,Direccion,Telefono,Celular,Email,Password,ConfirmPassword")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                db.SaveChanges();
                UsuariosMembershipController objM = new UsuariosMembershipController();
                objM.InsertarUsuarioMemberschip(empleados);
                return RedirectToAction("Index");
            }

            ViewBag.TipoDocumentoID = new SelectList(db.TipoDocumentos, "TipoDocumentoID", "DescriptionDocumento", empleados.TipoDocumentoID);
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoDocumentoID = new SelectList(db.TipoDocumentos, "TipoDocumentoID", "DescriptionDocumento", empleados.TipoDocumentoID);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEmpleado,TipoDocumentoID,NumeroDocumento,Nombres,PrimerApellido,SegundoApellido,Direccion,Telefono,Celular,Email,Password,ConfirmPassword")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                UsuariosMembershipController objM = new UsuariosMembershipController();
                objM.InsertarUsuarioMemberschip(empleados);
                return RedirectToAction("Index");
            }
            ViewBag.TipoDocumentoID = new SelectList(db.TipoDocumentos, "TipoDocumentoID", "DescriptionDocumento", empleados.TipoDocumentoID);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
