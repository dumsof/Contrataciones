using Contrataciones.Models;
using System;
using System.Collections.Generic;
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
            List<DenegarPermisos> listPermisoDenegado1 = (from me in dbContrata.Menus.ToList()
                                                          select new DenegarPermisos
                                                          {
                                                              DescripcionMenu = me.DescripcionMenu,
                                                              ControladorAccion = me.Controlador + "-" + me.Accion,
                                                              Permiso = true

                                                          }).ToList();

            List<DenegarPermisos> listPermisoDenegado2 = (from me in dbContrata.SubMenuOperaciones.ToList()
                                                          select new DenegarPermisos
                                                          {
                                                              DescripcionMenu = me.DescripcionOperacion,
                                                              ControladorAccion = me.Controlador + "-" + me.Accion,
                                                              Permiso = true
                                                          }).ToList();

            //union all para unificar menu principal y sub menu.
            List<DenegarPermisos> listResul = listPermisoDenegado1.Union(listPermisoDenegado2).ToList();

            return View(listResul);
        }

        // GET: DenegarPermisos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DenegarPermisos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DenegarPermisos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DenegarPermisos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DenegarPermisos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DenegarPermisos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DenegarPermisos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
