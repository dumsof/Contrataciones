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
    public class SubMenuOperacionesController : Controller
    {
        private ContextContratacion db = new ContextContratacion();

        // GET: SubMenuOperaciones
        public ActionResult Index()
        {
            var subMenuOperaciones = db.SubMenuOperaciones.Include(s => s.Menus);
            return View(subMenuOperaciones.ToList());
        }

        // GET: SubMenuOperaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenuOperaciones subMenuOperaciones = db.SubMenuOperaciones.Find(id);
            if (subMenuOperaciones == null)
            {
                return HttpNotFound();
            }
            return View(subMenuOperaciones);
        }

        // GET: SubMenuOperaciones/Create
        public ActionResult Create()
        {
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "DescripcionMenu");
            return View();
        }

        // POST: SubMenuOperaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubMenuOperacionID,MenuID,EsSubMenu,DescripcionOperacion,Controlador,Accion,OrdenamientoSubMenu")] SubMenuOperaciones subMenuOperaciones)
        {
            if (ModelState.IsValid)
            {
                db.SubMenuOperaciones.Add(subMenuOperaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "DescripcionMenu", subMenuOperaciones.MenuID);
            return View(subMenuOperaciones);
        }

        // GET: SubMenuOperaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenuOperaciones subMenuOperaciones = db.SubMenuOperaciones.Find(id);
            if (subMenuOperaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "DescripcionMenu", subMenuOperaciones.MenuID);
            return View(subMenuOperaciones);
        }

        // POST: SubMenuOperaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubMenuOperacionID,MenuID,EsSubMenu,DescripcionOperacion,Controlador,Accion,OrdenamientoSubMenu")] SubMenuOperaciones subMenuOperaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subMenuOperaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "DescripcionMenu", subMenuOperaciones.MenuID);
            return View(subMenuOperaciones);
        }

        // GET: SubMenuOperaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenuOperaciones subMenuOperaciones = db.SubMenuOperaciones.Find(id);
            if (subMenuOperaciones == null)
            {
                return HttpNotFound();
            }
            return View(subMenuOperaciones);
        }

        // POST: SubMenuOperaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubMenuOperaciones subMenuOperaciones = db.SubMenuOperaciones.Find(id);
            db.SubMenuOperaciones.Remove(subMenuOperaciones);
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
