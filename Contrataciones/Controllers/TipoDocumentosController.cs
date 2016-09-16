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
    public class TipoDocumentosController : Controller
    {
        private ContextContratacion db = new ContextContratacion();

        // GET: TipoDocumentos
        public ActionResult Index()
        {
            return View(db.TipoDocumentos.ToList());
        }

        // GET: TipoDocumentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumentos tipoDocumentos = db.TipoDocumentos.Find(id);
            if (tipoDocumentos == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocumentos);
        }

        // GET: TipoDocumentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDocumentos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoDocumentoID,DescriptionDocumento")] TipoDocumentos tipoDocumentos)
        {
            if (ModelState.IsValid)
            {
                db.TipoDocumentos.Add(tipoDocumentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDocumentos);
        }

        // GET: TipoDocumentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumentos tipoDocumentos = db.TipoDocumentos.Find(id);
            if (tipoDocumentos == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocumentos);
        }

        // POST: TipoDocumentos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoDocumentoID,DescriptionDocumento")] TipoDocumentos tipoDocumentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDocumentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoDocumentos);
        }

        // GET: TipoDocumentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocumentos tipoDocumentos = db.TipoDocumentos.Find(id);
            if (tipoDocumentos == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocumentos);
        }

        // POST: TipoDocumentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDocumentos tipoDocumentos = db.TipoDocumentos.Find(id);
            db.TipoDocumentos.Remove(tipoDocumentos);
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
