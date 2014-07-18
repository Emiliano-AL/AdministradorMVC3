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
    public class BoletoController : Controller
    {
        private rysi_adoEntities db = new rysi_adoEntities();

        //
        // GET: /Boleto/

        public ViewResult Index()
        {
            var boletos = db.boletos.Include(b => b.corrida1);
            return View(boletos.ToList());
        }

        //
        // GET: /Boleto/Details/5

        public ViewResult Details(string id)
        {
            boleto boleto = db.boletos.Find(id);
            return View(boleto);
        }

        //
        // GET: /Boleto/Create

        public ActionResult Create()
        {
            ViewBag.corrida = new SelectList(db.corridas, "folio", "folio");
            List<string> stalst = new List<string>();

            List<SelectListItem> statusLst = new List<SelectListItem>(){
                new SelectListItem() {Text="PAGADO", Value="PAGADO"},
                new SelectListItem() { Text="PENDIENTE", Value="PENDIENTE"},
                new SelectListItem() { Text="CANCELADO", Value="CANCELADO"}
                
            };
            ViewBag.estatus = new SelectList(statusLst, "Value", "Text");

            return View();
        } 

        //
        // POST: /Boleto/Create

        [HttpPost]
        public ActionResult Create(boleto boleto)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int folio = random.Next(0, 10000);
                string id = boleto.corrida;
                corrida corrida = db.corridas.Find(id);
                boleto.precio = corrida.precio;
                boleto.iva = corrida.iva;
                boleto.folio = folio.ToString();
                db.boletos.Add(boleto);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.corrida = new SelectList(db.corridas, "folio", "servicio", boleto.corrida);
            return View(boleto);
        }
        
        //
        // GET: /Boleto/Edit/5
 
        public ActionResult Edit(string id)
        {
            boleto boleto = db.boletos.Find(id);
            ViewBag.corrida = new SelectList(db.corridas, "folio", "servicio", boleto.corrida);
            return View(boleto);
        }

        //
        // POST: /Boleto/Edit/5

        [HttpPost]
        public ActionResult Edit(boleto boleto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boleto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.corrida = new SelectList(db.corridas, "folio", "servicio", boleto.corrida);
            return View(boleto);
        }

        //
        // GET: /Boleto/Delete/5
 
        public ActionResult Delete(string id)
        {
            boleto boleto = db.boletos.Find(id);
            return View(boleto);
        }

        //
        // POST: /Boleto/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            boleto boleto = db.boletos.Find(id);
            db.boletos.Remove(boleto);
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