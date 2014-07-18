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
    public class CiudadController : Controller
    {
        private rysi_adoEntities db = new rysi_adoEntities();

        //
        // GET: /Ciudad/

        public ViewResult Index()
        {
            var ciudades = db.ciudades.Include(c => c.estado);
            return View(ciudades.ToList());
        }

        //
        // GET: /Ciudad/Details/5

        public ViewResult Details(int id)
        {
            ciudade ciudade = db.ciudades.Find(id);
            return View(ciudade);
        }

        //
        // GET: /Ciudad/Create

        public ActionResult Create()
        {
            ViewBag.idestado = new SelectList(db.estados, "idestado", "nombre");
            return View();
        } 

        //
        // POST: /Ciudad/Create

        [HttpPost]
        public ActionResult Create(ciudade ciudade)
        {
            if (ModelState.IsValid)
            {
                db.ciudades.Add(ciudade);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idestado = new SelectList(db.estados, "idestado", "nombre", ciudade.idestado);
            return View(ciudade);
        }
        
        //
        // GET: /Ciudad/Edit/5
 
        public ActionResult Edit(int id)
        {
            ciudade ciudade = db.ciudades.Find(id);
            ViewBag.idestado = new SelectList(db.estados, "idestado", "nombre", ciudade.idestado);
            return View(ciudade);
        }

        //
        // POST: /Ciudad/Edit/5

        [HttpPost]
        public ActionResult Edit(ciudade ciudade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciudade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idestado = new SelectList(db.estados, "idestado", "nombre", ciudade.idestado);
            return View(ciudade);
        }

        //
        // GET: /Ciudad/Delete/5
 
        public ActionResult Delete(int id)
        {
            ciudade ciudade = db.ciudades.Find(id);
            return View(ciudade);
        }

        //
        // POST: /Ciudad/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ciudade ciudade = db.ciudades.Find(id);
            db.ciudades.Remove(ciudade);
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