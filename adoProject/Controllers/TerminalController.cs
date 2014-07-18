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
    public class TerminalController : Controller
    {
        private rysi_adoEntities db = new rysi_adoEntities();

        //
        // GET: /Terminal/

        public ViewResult Index()
        {
            var terminales = db.terminales.Include(t => t.ciudade);
            return View(terminales.ToList());
        }

        //
        // GET: /Terminal/Details/5

        public ViewResult Details(int id)
        {
            terminale terminale = db.terminales.Find(id);
            return View(terminale);
        }

        //
        // GET: /Terminal/Create

        public ActionResult Create()
        {
            ViewBag.idciudad = new SelectList(db.ciudades, "idciudad", "nombre");
            return View();
        } 

        //
        // POST: /Terminal/Create

        [HttpPost]
        public ActionResult Create(terminale terminale)
        {
            if (ModelState.IsValid)
            {
                db.terminales.Add(terminale);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idciudad = new SelectList(db.ciudades, "idciudad", "nombre", terminale.idciudad);
            return View(terminale);
        }
        
        //
        // GET: /Terminal/Edit/5
 
        public ActionResult Edit(int id)
        {
            terminale terminale = db.terminales.Find(id);
            ViewBag.idciudad = new SelectList(db.ciudades, "idciudad", "nombre", terminale.idciudad);
            return View(terminale);
        }

        //
        // POST: /Terminal/Edit/5

        [HttpPost]
        public ActionResult Edit(terminale terminale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terminale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idciudad = new SelectList(db.ciudades, "idciudad", "nombre", terminale.idciudad);
            return View(terminale);
        }

        //
        // GET: /Terminal/Delete/5
 
        public ActionResult Delete(int id)
        {
            terminale terminale = db.terminales.Find(id);
            return View(terminale);
        }

        //
        // POST: /Terminal/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            terminale terminale = db.terminales.Find(id);
            db.terminales.Remove(terminale);
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