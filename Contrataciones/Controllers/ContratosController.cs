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
    public class ContratosController : Controller
    {
        private ContextContratacion db = new ContextContratacion();

        // GET: Contratos
        public ActionResult Index()
        {
            var contratos = db.Contratos.Include(c => c.Empleado);
            return View(contratos.ToList());
        }

        // GET: Contratos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contratos contratos = db.Contratos.Find(id);
            if (contratos == null)
            {
                return HttpNotFound();
            }
            return View(contratos);
        }

        // GET: Contratos/Create
        public ActionResult Create()
        {
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "NumeroDocumento");
            return View();
        }

        // POST: Contratos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContratoID,IdEmpleado,DisponibilidadPresupuestalCDP,CompromisoPresupuestalRP,FechaInicio,FechaFinal,ValorContrato")] Contratos contratos)
        {
            if (ModelState.IsValid)
            {
                db.Contratos.Add(contratos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "NumeroDocumento", contratos.IdEmpleado);
            return View(contratos);
        }

        // GET: Contratos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contratos contratos = db.Contratos.Find(id);
            if (contratos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "NumeroDocumento", contratos.IdEmpleado);
            return View(contratos);
        }

        // POST: Contratos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContratoID,IdEmpleado,DisponibilidadPresupuestalCDP,CompromisoPresupuestalRP,FechaInicio,FechaFinal,ValorContrato")] Contratos contratos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contratos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "NumeroDocumento", contratos.IdEmpleado);
            return View(contratos);
        }

        // GET: Contratos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contratos contratos = db.Contratos.Find(id);
            if (contratos == null)
            {
                return HttpNotFound();
            }
            return View(contratos);
        }

        // POST: Contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contratos contratos = db.Contratos.Find(id);
            db.Contratos.Remove(contratos);
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
