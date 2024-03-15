using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using pizzerona.Models;

namespace pizzerona.Controllers
{
    public class ORDINEsController : Controller
    {
        private Model1 db = new Model1();

        //GET: ORDINEs
        public ActionResult Index()
        {
            //    var cookieValue = HttpContext.Request.Cookies["IDCookie"]?.Value;
            //    int IDCOOKIE = Convert.ToInt32(cookieValue);

            //    var cookieVaLORE = HttpContext.Request.Cookies["PizzaCookie"]?.Value;
            //    int cookieNumber = Convert.ToInt32(cookieVaLORE); // Utilizza cookieVaLORE invece di cookieValue

            //    var oRDINE = db.ORDINE.Add(o => o.FK_ID_CLIENTE == IDCOOKIE).Where(o => o.FK_ID_PIZZA == cookieNumber).Include(o => o.BIBITE).Include(o => o.CLIENTE).Include(o => o.Pizze);

            return View("index");
    }
        [HttpPost]
        public ActionResult AggiungiAlCarrello(int idPizza, string quantita)
        {

            int quantitaInt = Convert.ToInt32(quantita);
            // Recupera la pizza dal database utilizzando l'ID
            var pizza = db.Pizze.FirstOrDefault(p => p.id_Pizza == idPizza);

               var cookieValue = HttpContext.Request.Cookies["IDCookie"]?.Value;
                int IDCOOKIE = Convert.ToInt32(cookieValue);
            var indizzo = db.CLIENTE.FirstOrDefault(c => c.ID_CLIENTE == IDCOOKIE);

            if (pizza != null)
            {
                // Aggiungi la pizza al carrello
                var elementoOrdine = new ORDINE {FK_ID_CLIENTE= IDCOOKIE, FK_ID_PIZZA = idPizza, QUANTITA = quantitaInt, INDIRIZZO_CONSEGNA= "via IPPOLITO" }; // Puoi personalizzare la quantità in base alle tue esigenze
                db.ORDINE.Add(elementoOrdine);
                db.SaveChanges();// Ritorna un'esito positivo o un messaggio di successo, a seconda delle tue esigenze
               
            }

            // Se la pizza non viene trovata nel database, ritorna un'esito negativo o un messaggio di errore
            return RedirectToAction("Index","Pizze");
        }

        // GET: ORDINEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDINE oRDINE = db.ORDINE.Find(id);
            if (oRDINE == null)
            {
                return HttpNotFound();
            }
            return View(oRDINE);
        }

        // GET: ORDINEs/Create
        public ActionResult Create()
        {
            ViewBag.FK_ID_BIBITA = new SelectList(db.BIBITE, "ID_BIBITA", "NOME");
            ViewBag.FK_ID_CLIENTE = new SelectList(db.CLIENTE, "ID_CLIENTE", "NOME");
            ViewBag.FK_ID_PIZZA = new SelectList(db.Pizze, "id_Pizza", "Nome");
            return View();
        }



        // POST: ORDINEs/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ORDINE,FK_ID_PIZZA,FK_ID_BIBITA,FK_ID_CLIENTE,INDIRIZZO_CONSEGNA,QUANTITA,NOTA,TOTALE")] ORDINE oRDINE)
        {
            if (ModelState.IsValid)
            {
                db.ORDINE.Add(oRDINE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_ID_BIBITA = new SelectList(db.BIBITE, "ID_BIBITA", "NOME", oRDINE.FK_ID_BIBITA);
            ViewBag.FK_ID_CLIENTE = new SelectList(db.CLIENTE, "ID_CLIENTE", "NOME", oRDINE.FK_ID_CLIENTE);
            ViewBag.FK_ID_PIZZA = new SelectList(db.Pizze, "id_Pizza", "Nome", oRDINE.FK_ID_PIZZA);
            return View(oRDINE);
        }

        // GET: ORDINEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDINE oRDINE = db.ORDINE.Find(id);
            if (oRDINE == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_ID_BIBITA = new SelectList(db.BIBITE, "ID_BIBITA", "NOME", oRDINE.FK_ID_BIBITA);
            ViewBag.FK_ID_CLIENTE = new SelectList(db.CLIENTE, "ID_CLIENTE", "NOME", oRDINE.FK_ID_CLIENTE);
            ViewBag.FK_ID_PIZZA = new SelectList(db.Pizze, "id_Pizza", "Nome", oRDINE.FK_ID_PIZZA);
            return View(oRDINE);
        }

        // POST: ORDINEs/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ORDINE,FK_ID_PIZZA,FK_ID_BIBITA,FK_ID_CLIENTE,INDIRIZZO_CONSEGNA,QUANTITA,NOTA,TOTALE")] ORDINE oRDINE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDINE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_ID_BIBITA = new SelectList(db.BIBITE, "ID_BIBITA", "NOME", oRDINE.FK_ID_BIBITA);
            ViewBag.FK_ID_CLIENTE = new SelectList(db.CLIENTE, "ID_CLIENTE", "NOME", oRDINE.FK_ID_CLIENTE);
            ViewBag.FK_ID_PIZZA = new SelectList(db.Pizze, "id_Pizza", "Nome", oRDINE.FK_ID_PIZZA);
            return View(oRDINE);
        }

        // GET: ORDINEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDINE oRDINE = db.ORDINE.Find(id);
            if (oRDINE == null)
            {
                return HttpNotFound();
            }
            return View(oRDINE);
        }

        // POST: ORDINEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ORDINE oRDINE = db.ORDINE.Find(id);
            db.ORDINE.Remove(oRDINE);
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
