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
    public class AutobusController : Controller
    {
        private rysi_adoEntities db = new rysi_adoEntities();

        //
        // GET: /Autobus/

        public ViewResult Index()
        {
            return View(db.autobuses.ToList());
        }

        //
        // GET: /Autobus/Details/5

        public ViewResult Details(string id)
        {
            autobus autobus = db.autobuses.Find(id);
            return View(autobus);
        }

        //
        // GET: /Autobus/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Autobus/Create

        [HttpPost]
        public ActionResult Create(autobus autobus)
        {
            if (ModelState.IsValid)
            {
                db.autobuses.Add(autobus);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(autobus);
        }
        
        //
        // GET: /Autobus/Edit/5
 
        public ActionResult Edit(string id)
        {
            autobus autobus = db.autobuses.Find(id);
            return View(autobus);
        }

        //
        // POST: /Autobus/Edit/5

        [HttpPost]
        public ActionResult Edit(autobus autobus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autobus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autobus);
        }

        //
        // GET: /Autobus/Delete/5
 
        public ActionResult Delete(string id)
        {
            autobus autobus = db.autobuses.Find(id);
            return View(autobus);
        }

        //
        // POST: /Autobus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            autobus autobus = db.autobuses.Find(id);
            db.autobuses.Remove(autobus);
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