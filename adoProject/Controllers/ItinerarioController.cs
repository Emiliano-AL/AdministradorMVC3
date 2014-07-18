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
    public class ItinerarioController : Controller
    {
        private rysi_adoEntities db = new rysi_adoEntities();

        //
        // GET: /Itinerario/

        public ViewResult Index()
        {
            var itineriarios = db.itineriarios.Include(i => i.terminale).Include(i => i.terminale1);
            return View(itineriarios.ToList());
        }

        //
        // GET: /Itinerario/Details/5

        public ViewResult Details(int id)
        {
            itineriario itineriario = db.itineriarios.Find(id);
            return View(itineriario);
        }

        //
        // GET: /Itinerario/Create

        public ActionResult Create()
        {
            ViewBag.destino = new SelectList(db.terminales, "idterminal", "nombre");
            ViewBag.origen = new SelectList(db.terminales, "idterminal", "nombre");
            return View();
        } 

        //
        // POST: /Itinerario/Create

        [HttpPost]
        public ActionResult Create(itineriario itineriario)
        {
            if (ModelState.IsValid)
            {
                db.itineriarios.Add(itineriario);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.destino = new SelectList(db.terminales, "idterminal", "nombre", itineriario.destino);
            ViewBag.origen = new SelectList(db.terminales, "idterminal", "nombre", itineriario.origen);
            return View(itineriario);
        }
        
        //
        // GET: /Itinerario/Edit/5
 
        public ActionResult Edit(int id)
        {
            itineriario itineriario = db.itineriarios.Find(id);
            ViewBag.destino = new SelectList(db.terminales, "idterminal", "nombre", itineriario.destino);
            ViewBag.origen = new SelectList(db.terminales, "idterminal", "nombre", itineriario.origen);
            return View(itineriario);
        }

        //
        // POST: /Itinerario/Edit/5

        [HttpPost]
        public ActionResult Edit(itineriario itineriario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itineriario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.destino = new SelectList(db.terminales, "idterminal", "nombre", itineriario.destino);
            ViewBag.origen = new SelectList(db.terminales, "idterminal", "nombre", itineriario.origen);
            return View(itineriario);
        }

        //
        // GET: /Itinerario/Delete/5
 
        public ActionResult Delete(int id)
        {
            itineriario itineriario = db.itineriarios.Find(id);
            return View(itineriario);
        }

        //
        // POST: /Itinerario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            itineriario itineriario = db.itineriarios.Find(id);
            db.itineriarios.Remove(itineriario);
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