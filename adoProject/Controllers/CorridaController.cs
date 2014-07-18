using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using adoProject.Models;

namespace adoProject.Controllers
{ 
    public class CorridaController : Controller
    {
        private rysi_adoEntities db = new rysi_adoEntities();

        //
        // GET: /Corrida/

        public ViewResult Index()
        {
            var corridas = db.corridas.Include(c => c.itineriario);
            return View(corridas.ToList());
        }

        //
        // GET: /Corrida/Details/5

        public ViewResult Details(string id)
        {
            corrida corrida = db.corridas.Find(id);
            return View(corrida);
        }

        //
        // GET: /Corrida/Create

        public ActionResult Create()
        {
            ViewBag.itinerario = new SelectList(db.itineriarios, "id", "id");
            ViewBag.origen = new SelectList(db.terminales, "idTerminal", "nombre");
            ViewBag.destino = new SelectList(db.terminales, "idTerminal", "nombre");

            List<SelectListItem> servlst = new List<SelectListItem>(){
                new SelectListItem() {Text="GRAN_LUJO", Value="GRAN_LUJO"},
                new SelectListItem() { Text="PRIMERA", Value="PRIMERA"},
                new SelectListItem() { Text="TURISTA", Value="TURISTA"}
            };
            ViewBag.servicio = new SelectList(servlst, "Value", "Text");

            return View();
        } 

        //
        // POST: /Corrida/Create

        [HttpPost]
        public ActionResult Create(corrida corrida)
        {
            if (ModelState.IsValid)
            {
                int ori = Convert.ToInt32(corrida.origen);
                int dest = Convert.ToInt32(corrida.destino);

                try
                {
                    corrida.itinerario = db.itineriarios.Where(x => x.origen == ori && x.destino == dest).Select(x => x.id).First();
                    //itineriario iti = db.itineriarios.Where(x => x.origen == ori && x.destino == dest).First();
                    corrida.estatus = "DISPONIBLE";
                    //corrida.servicio = "PRIMERA";
                    Random random = new Random();
                    int folio = random.Next(0,10000);
                    corrida.folio = folio.ToString();
                    corrida.total = 45;
                    corrida.fecha = DateTime.Now;
                    corrida.disponibles = 40;
                    corrida.iva = calcularIva(corrida.precio, 16);
                    corrida.total = corrida.precio + corrida.iva;
                }
                catch (Exception e)
                {
                    corrida.itinerario = -1;
                }
                if (corrida.itinerario == -1)
                {
                    ViewBag.error = "La ruta de salida y llegada no existen";
                    RedirectToAction("Create");
                }
                else
                {
                    db.corridas.Add(corrida);
                    db.SaveChanges();
                    ViewBag.error = "";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.itinerario = new SelectList(db.itineriarios, "id", "id", corrida.itinerario);
            return View(corrida);
        }

        private decimal calcularIva(decimal precio, int tax)
        {
            return (precio * tax) / 100;
        }
        
        //
        // GET: /Corrida/Edit/5
 
        public ActionResult Edit(string id)
        {
            corrida corrida = db.corridas.Find(id);
            ViewBag.itinerario = new SelectList(db.itineriarios, "id", "id", corrida.itinerario);
            return View(corrida);
        }

        //
        // POST: /Corrida/Edit/5

        [HttpPost]
        public ActionResult Edit(corrida corrida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corrida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.itinerario = new SelectList(db.itineriarios, "id", "id", corrida.itinerario);
            return View(corrida);
        }

        //
        // GET: /Corrida/Delete/5
 
        public ActionResult Delete(string id)
        {
            corrida corrida = db.corridas.Find(id);
            return View(corrida);
        }

        //
        // POST: /Corrida/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            corrida corrida = db.corridas.Find(id);
            db.corridas.Remove(corrida);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}