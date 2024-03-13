using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pizzerona.Models;

namespace pizzerona.Controllers
{
    [Authorize(Roles="Admin")]
    public class PizzesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Pizzes
        public ActionResult Index()
        {
            return View(db.Pizze.ToList());
        }

        // GET: Pizzes/Details/5
        public ActionResult Details(int? id)

        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizze = db.Pizze.Find(id);
            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View(pizze);
        }

        // GET: Pizzes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizzes/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Pizza,Nome,img,Prezzo,TempoConsegna,Ingredienti")] Pizze pizze, HttpPostedFileBase img )
        {


            if (img != null && img.ContentLength > 0)
            {
                string nomeFile = Path.GetFileName(img.FileName);
                string pathToSave = Path.Combine(Server.MapPath("~/img/"), nomeFile);
                img.SaveAs(pathToSave);
                pizze.img = "/img/" + nomeFile;
            }

            if (ModelState.IsValid)
            {
                db.Pizze.Add(pizze);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pizze);
        }

        // GET: Pizzes/Edit/5

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // un writeline non hai scelto niente
            }
            var NuovaPizza = db.Pizze.Find(id);


            System.Diagnostics.Debug.WriteLine($"ìììììììììììììììììììììììììììììììììììììììììììììììììììììììììììììììValore di Piazza ID: {NuovaPizza}");

            if (NuovaPizza == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        // POST: Pizzes/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Pizza,Nome,img,Prezzo,TempoConsegna,Ingredienti")] ORDINE ordine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(ordine);
        }

        // GET: Pizzes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizze = db.Pizze.Find(id);
            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View(pizze);
        }

        // POST: Pizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizze pizze = db.Pizze.Find(id);
            db.Pizze.Remove(pizze);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
