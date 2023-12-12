using PWA_Proyecto2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PWA_Proyecto2.Controllers
{
    public class AvionesController : Controller
    {
        private const string AvionesKey = "Aviones";

        // GET: Aviones
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {

                var retiros = context.RetiroAviones.ToList();
                ViewBag.Retiros = retiros;

                return View(context.Aviones.Where(a => !a.Retirado.HasValue || (a.Retirado.HasValue && !a.Retirado.Value)).ToList());
            }
        }

        // GET: Aviones/Create
        public ActionResult Create()
        {
            using (DbModels context = new DbModels())
            {
                List<Aviones> aviones = ObtenerAviones();
                List<MarcasAviones> marcasList = context.MarcasAviones.ToList();
                ViewBag.Aviones = aviones;
                ViewData["MarcaId"] = new SelectList(marcasList, "IdMarca", "Nombre");
                return View();
            }
        }

        public JsonResult GetModelosByMarca(int idMarca)
        {
            using (DbModels context = new DbModels())
            {
                var modelos = context.ModelosAviones
                    .Where(a => a.MarcaId == idMarca)
                    .Select(a => new { Value = a.ModeloId, Text = a.Nombre })
                    .Distinct()
                    .ToList();

                return Json(modelos, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Aviones/Create
        [HttpPost]
        public ActionResult Create(Aviones aviones)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    if (Session[AvionesKey] != null)
                    {
                        var avionesEnSesion = (List<Aviones>)Session[AvionesKey];

                        foreach (var avionEnSesion in avionesEnSesion)
                        {
                            context.Aviones.Add(avionEnSesion);
                            avionEnSesion.FechaIngreso = DateTime.Now;
                            avionEnSesion.Retirado = false;
                        }

                        Session.Remove(AvionesKey);
                    }

                    context.Aviones.Add(aviones);
                    aviones.FechaIngreso = DateTime.Now;
                    aviones.Retirado = false;

                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private List<Aviones> ObtenerAviones()
        {
            List<Aviones> aviones = Session[AvionesKey] as List<Aviones>;

            if (aviones == null)
            {
                aviones = new List<Aviones>();
            }

            return aviones;
        }

        [HttpPost]
        public ActionResult CreateState(Aviones aviones)
        {
            List<Aviones> avion = ObtenerAviones();
            if (avion == null)
            {
                avion = new List<Aviones>();
            }
            avion.Add(aviones);
            Session[AvionesKey] = avion;
            return RedirectToAction("Create");
        }


        // POST: Aviones/Retiro
        public ActionResult Retiro()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetListaAviones()
        {
            using (DbModels context = new DbModels())
            {
                var listaAviones = context.Aviones
                    .Where(a => !a.Retirado.HasValue || (a.Retirado.HasValue && !a.Retirado.Value))
                    .Select(a => new
                    {
                        a.NumeroSerie,
                    })
                .ToList();

                return Json(listaAviones, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Retiro(RetiroAviones retiroAviones)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    var avionEncontrado = context.Aviones.FirstOrDefault(avion => avion.NumeroSerie == retiroAviones.NumeroSerie);
                    if (avionEncontrado != null && !avionEncontrado.Retirado.HasValue)
                    {
                        avionEncontrado.Retirado = true;

                        context.SaveChanges();

                        retiroAviones.FechaRetiro = DateTime.Now;
                        context.RetiroAviones.Add(retiroAviones);
                        context.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
