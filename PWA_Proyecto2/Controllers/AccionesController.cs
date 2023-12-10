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
                var aterrizajes = context.Aterrizaje.ToList();
                var despegues = context.Despegue.ToList();
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
                int consecutivo = context.Despegue.Count();
                int year = DateTime.Now.Year;
                if (consecutivo == 0 )
                {
                    consecutivo = 1;
                }
                else
                {
                    consecutivo = consecutivo + 1;
                }

                string numeroDespegue = $"{year}-DE-{consecutivo:D6}";

                return numeroDespegue;
            }
        }

        // GET: Acciones/Create
        public ActionResult CrearAterrizaje()
        {
            using (DbModels context = new DbModels())
            {
                var despegues = context.Despegue.ToList();
                SelectList despeguesSelectList = new SelectList(despegues, "NumeroDespegue", "NumeroDespegue");

                ViewBag.DespeguesSelectList = despeguesSelectList;

                return View();
            }
        }

        // POST: Acciones/Create
        [HttpPost]
        public ActionResult CrearAterrizaje(Aterrizaje aterrizaje)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Aterrizaje.Add(aterrizaje);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
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
