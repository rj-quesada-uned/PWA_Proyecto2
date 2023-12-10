using PWA_Proyecto2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PWA_Proyecto2.Controllers
{

    public class AccionesController : Controller
    {
        // GET: Acciones
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {
                var despegues = context.Aterrizaje.ToList();
                var aterrizajes = context.Despegue.ToList();
                ViewBag.Despegues = despegues;
                ViewBag.Aterrizajes = aterrizajes;

                return View();
            }

        }

        // GET: Acciones/Create
        public ActionResult CrearDespegue()
        {
            return View();
        }

        // POST: Acciones/Create
        [HttpPost]
        public ActionResult CrearDespegue(Despegue despegue)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Despegue.Add(despegue);
                    despegue.NumeroDespegue = GenerarNumeroDespegue();
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al guardar: {ex.Message}");

                return View(despegue);
            }
        }
        private string GenerarNumeroDespegue()
        {
            using (DbModels context = new DbModels())
            {
                var ultimoElemento = context.Despegue
                    .OrderByDescending(e => e.Id)
                    .FirstOrDefault();
                int year = DateTime.Now.Year;
                int consecutivo = 1;

                if (ultimoElemento != null)
                {
                    consecutivo = ultimoElemento.Id+1;
                }

                string numeroDespegue = $"{year}-DE-{consecutivo:D6}";

                return numeroDespegue;
            }
        }

        // GET: Acciones/Create
        public ActionResult CrearAterrizaje()
        {
            return View();
        }

        // POST: Acciones/Create
        [HttpPost]
        public ActionResult CrearAterrizaje(FormCollection collection)
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

        // GET: Acciones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Acciones/Edit/5
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

        // GET: Acciones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Acciones/Delete/5
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
