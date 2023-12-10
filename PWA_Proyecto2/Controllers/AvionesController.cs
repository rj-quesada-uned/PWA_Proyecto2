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
        // GET: Aviones
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Aviones.ToList());
            }
        }

        // GET: Aviones/Create
        public ActionResult Create()
        {
            using (DbModels context = new DbModels())
            {
                List<MarcasAviones> marcasList = context.MarcasAviones.ToList();
                ViewBag.ListaMarcas = new SelectList(marcasList, "IdMarca", "Nombre");
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
                    context.Aviones.Add(aviones);
                    aviones.FechaIngreso = DateTime.Now;
                    context.SaveChanges();
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
