using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pizzerona.Models;

namespace pizzerona.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BIBITEsController : Controller
    {
        private Model1 db = new Model1();

        // GET: BIBITEs
        public ActionResult Index()
        {
            return View(db.BIBITE.ToList());
        }

        // GET: BIBITEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBITE bIBITE = db.BIBITE.Find(id);
            if (bIBITE == null)
            {
                return HttpNotFound();
            }
            return View(bIBITE);
        }

        // GET: BIBITEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BIBITEs/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BIBITA,NOME,IMG,PREZZO")] BIBITE bIBITE)
        {
            if (ModelState.IsValid)
            {
                db.BIBITE.Add(bIBITE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bIBITE);
        }

        // GET: BIBITEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBITE bIBITE = db.BIBITE.Find(id);
            if (bIBITE == null)
            {
                return HttpNotFound();
            }
            return View(bIBITE);
        }

        // POST: BIBITEs/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BIBITA,NOME,IMG,PREZZO")] BIBITE bIBITE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bIBITE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bIBITE);
        }

        // GET: BIBITEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBITE bIBITE = db.BIBITE.Find(id);
            if (bIBITE == null)
            {
                return HttpNotFound();
            }
            return View(bIBITE);
        }

        // POST: BIBITEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BIBITE bIBITE = db.BIBITE.Find(id);
            db.BIBITE.Remove(bIBITE);
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
