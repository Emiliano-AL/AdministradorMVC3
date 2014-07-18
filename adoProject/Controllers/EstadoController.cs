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
    public class EstadoController : Controller
    {
        private rysi_adoEntities db = new rysi_adoEntities();

        //
        // GET: /Estado/

        public ViewResult Index()
        {
            return View(db.estados.ToList());
        }

        //
        // GET: /Estado/Details/5

        public ViewResult Details(int id)
        {
            estado estado = db.estados.Find(id);
            return View(estado);
        }

        //
        // GET: /Estado/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Estado/Create

        [HttpPost]
        public ActionResult Create(estado estado)
        {
            if (ModelState.IsValid)
            {
                db.estados.Add(estado);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(estado);
        }
        
        //
        // GET: /Estado/Edit/5
 
        public ActionResult Edit(int id)
        {
            estado estado = db.estados.Find(id);
            return View(estado);
        }

        //
        // POST: /Estado/Edit/5

        [HttpPost]
        public ActionResult Edit(estado estado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estado);
        }

        //
        // GET: /Estado/Delete/5
 
        public ActionResult Delete(int id)
        {
            estado estado = db.estados.Find(id);
            return View(estado);
        }

        //
        // POST: /Estado/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            estado estado = db.estados.Find(id);
            db.estados.Remove(estado);
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