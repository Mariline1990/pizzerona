using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using pizzerona.Models;

namespace pizzerona.Controllers
{
    public class ORDINEsController : Controller
    {
        private Model1 db = new Model1();

        //GET: ORDINEs
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var Acquisti = db.ORDINE.Where(o => o.NOTA == "Acquistato").ToList();

            return View(Acquisti.ToList());
        }



        public ActionResult Evaso(int id)
        {
            var cookieValue = HttpContext.Request.Cookies["IDCookie"]?.Value;
            int idCliente = Convert.ToInt32(cookieValue);

            // Trova l'ordine dell'ID specificato per il cliente corrente
            var ordine = db.ORDINE.FirstOrDefault(o => o.ID_ORDINE == id && o.FK_ID_CLIENTE == idCliente);

           
                // Modifica lo stato dell'ordine
                ordine.NOTA = "Evaso";

                // Salva le modifiche nel database
                db.SaveChanges();


            // Ritorna alla vista precedente o ad un'altra vista
            return View(ordine);
        }

      
        [HttpPost]
        public ActionResult AggiungiAlCarrello(int idPizza, string quantita)
        {

            int quantitaInt = Convert.ToInt32(quantita);
            // Recupera la pizza dal database utilizzando l'ID
            var pizza = db.Pizze.FirstOrDefault(p => p.id_Pizza == idPizza);

            var prezzoPizza = db.Pizze.Where(p => p.id_Pizza == idPizza)
                              .Select(prodotto => prodotto.Prezzo)
                              .FirstOrDefault();
            int prezzoInt = Convert.ToInt32(prezzoPizza);
            int totale = prezzoInt * quantitaInt;


            var cookieValue = HttpContext.Request.Cookies["IDCookie"]?.Value;
                int IDCOOKIE = Convert.ToInt32(cookieValue);
            var indizzo = db.CLIENTE.FirstOrDefault(c => c.ID_CLIENTE == IDCOOKIE);

            if (pizza != null)
            {
                // Aggiungi la pizza al carrello
                var elementoOrdine = new ORDINE {FK_ID_CLIENTE= IDCOOKIE, FK_ID_PIZZA = idPizza, QUANTITA = quantitaInt, INDIRIZZO_CONSEGNA= "via IPPOLITO" , NOTA="In corso", TOTALE = totale }; // Puoi personalizzare la quantità in base alle tue esigenze
                db.ORDINE.Add(elementoOrdine);
                db.SaveChanges();// Ritorna un'esito positivo o un messaggio di successo, a seconda delle tue esigenze
               
            }

          
            return RedirectToAction("Index","Pizze");
        }


        // tornare a fare la vista con la classe dettagli  di ordine /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// STEP 2
        [Authorize(Roles = "User")]
        public ActionResult AggiungiAlCarrello()
        {
            var cookieValue = HttpContext.Request.Cookies["IDCookie"]?.Value;
            int IDCOOKIE = Convert.ToInt32(cookieValue);
            int idCLiente = IDCOOKIE;
            System.Diagnostics.Debug.WriteLine($"SONO CLIENTE == N :{idCLiente}");

            // Seleziona gli ordini in base all'ID del cliente
            var ordini = db.ORDINE.Where(o => o.FK_ID_CLIENTE == idCLiente).ToList();

            var totale = ordini.Sum(o => o.TOTALE);
            ViewBag.Totale = totale;


            return View(ordini);
        }
        [Authorize(Roles = "User")]
        public ActionResult ModificaStato()
        {
            var cookieValue = HttpContext.Request.Cookies["IDCookie"]?.Value;
            int IDCOOKIE = Convert.ToInt32(cookieValue);
            int idCliente = IDCOOKIE;

            // Trova l'ordine relativo al cliente
            var ordini = db.ORDINE.Where(o => o.FK_ID_CLIENTE == idCliente).ToList();

            // Modifica la proprietà NOTA per ciascun ordine
            foreach (var ordine in ordini)
            {
                ordine.NOTA = "Acquistato";
            }

            // Salva le modifiche nel database
            db.SaveChanges();

            return RedirectToAction("AggiungiAlCarrello");
        }


        [Authorize(Roles = "Admin")]
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
       
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
