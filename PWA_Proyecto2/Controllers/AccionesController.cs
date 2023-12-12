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
        private const string DespeguesKey = "Despegues";

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
            List<Despegue> despegues = ObtenerDespegues();
            ViewBag.Despegues = despegues;

            return View();
        }

        // POST: Acciones/Create
        [HttpPost]
        public ActionResult CrearDespegue(Despegue despegue)
        {
            int consecutivo = GenerarNumeroDespegue();
            int year = DateTime.Now.Year;
            string numeroDespegue = $"{year}-DE-{consecutivo:D6}";

            try
            {
                using (DbModels context = new DbModels())
                {
                    if (Session[DespeguesKey] != null)
                    {
                        var despeguesEnSesion = (List<Despegue>)Session[DespeguesKey];

                        foreach (var despegueEnSesion in despeguesEnSesion)
                        {
                            context.Despegue.Add(despegueEnSesion);
                        }

                        Session.Remove(DespeguesKey);
                    }

                    despegue.NumeroDespegue = numeroDespegue;
                    context.Despegue.Add(despegue);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(despegue);
            }
        }
        private List<Despegue> ObtenerDespegues()
        {
            List<Despegue> despegue = Session[DespeguesKey] as List<Despegue>;

            if (despegue == null)
            {
                despegue = new List<Despegue>();
            }

            return despegue;
        }

        [HttpPost]
        public ActionResult CreateState(Despegue despegues)
        {
            List<Despegue> despegue = ObtenerDespegues();
            int consecutivo = GenerarNumeroDespegue();
            int year = DateTime.Now.Year;
            string numeroDespegue = "";
            if (despegue == null)
            {
                despegue = new List<Despegue>();
                numeroDespegue = $"{year}-DE-{consecutivo:D6}";
            }
            else
            {
                int sumaConsecutivo = consecutivo + despegue.Count();
                numeroDespegue = $"{year}-DE-{sumaConsecutivo:D6}";
            }

            despegues.NumeroDespegue = numeroDespegue;

            despegue.Add(despegues);

            Session[DespeguesKey] = despegue;
            return RedirectToAction("CrearDespegue");
        }

        private int GenerarNumeroDespegue()
        {
            using (DbModels context = new DbModels())
            {
                int consecutivo = context.Despegue.Count();
                if (consecutivo == 0 )
                {
                    consecutivo = 1;
                }
                else
                {
                    consecutivo = consecutivo + 1;
                }

                return consecutivo;
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
