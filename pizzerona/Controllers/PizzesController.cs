using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;

using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using pizzerona.Models;

namespace pizzerona.Controllers
{
   // [Authorize(Roles="Admin")]
    public class PizzesController : Controller
    {
     
        private Model1 db = new Model1();

        // GET: Pizzes
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {

        //   var ordiniCliente = db.ORDINE.Where(o => o.FK_ID_CLIENTE == idCliente).Include(o => o.Pizze);
        
          //  System.Diagnostics.Debug.WriteLine($"SONO IL VALORE DI ORDINI: {PIZO}");

            return View(db.Pizze.ToList());
        }

        // GET: Pizzes/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizzes/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // un writeline non hai scelto niente
            }
            var pizze = db.Pizze.Find(id);
      

            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View("Edit", pizze);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "id_Pizza,Nome,img,Prezzo,TempoConsegna,Ingredienti")] Pizze pizze)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pizze).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizze);
        }



        // COMES SE NON ENTRASSE
      

        [HttpGet]
        public ActionResult AddPizza(int? id)
        {
            if (id == null)
            {
                System.Diagnostics.Debug.WriteLine($"Valore di id nullo");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            HttpCookie PizzaCookie = new HttpCookie("PizzaCookie");           
            PizzaCookie.Value = $"{id }"; 
            PizzaCookie.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(PizzaCookie);

            Pizze pizze = db.Pizze.Find(id);

            // Verifica se la pizza esiste nel database
            if (pizze == null)
            {
                
                return HttpNotFound();
            }
            // NELLE VIEW LIST RICORDARSI SEMPRE DI PASSARE UNA LISTA!!!!
            return View(new List<Pizze> { pizze });


        }


        //[HttpPost]
        //public ActionResult Add()
        //{

        //        var cookieValue = HttpContext.Request.Cookies["IDCookie"]?.Value;
        //        var cookiepizz = HttpContext.Request.Cookies["PizzaCookie"]?.Value;

        //    using (Model1 dbContext = new Model1())
        //    {
        //        db.ORDINE.Add(new ORDINE
        //            {
        //                FK_ID_PIZZA = Convert.ToInt32(cookiepizz),
        //                FK_ID_CLIENTE = Convert.ToInt32(cookieValue),
        //                INDIRIZZO_CONSEGNA = "Via Roma 1",
        //                QUANTITA = 1,
        //                TOTALE = 5,
        //                NOTA = "Nessuna nota"

        //            } );


        //        dbContext.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //}

        //[HttpPost]
        //public ActionResult AddPizza()
        //{

        //    System.Diagnostics.Debug.WriteLine($"SONO IL TUO AGGIUNGI");

        //    using (var dbContext = new Model1())
        //    {

        //        //  var cookieValue = HttpContext.Request.Cookies["PizzaCookie"]?.Value;

        //        //  System.Diagnostics.Debug.WriteLine($"SONO IL VALORE DI NUOVOORDINE: {cookieValue}");
        //        //   var cookieValue = HttpContext.Request.Cookies["PizzaCookie"]?.Value;

        //        ORDINE NuovoOrdine = new ORDINE()
        //        {
        //            FK_ID_PIZZA = Convert.ToInt32(Request.Cookies["PizzaCookie"].Value),

        //        };

        //        // Aggiunta del nuovo ordine al set di entità ORDINE
        //        dbContext.ORDINE.Add(NuovoOrdine);
        //        dbContext.SaveChanges();

        //        // Applicazione delle modifiche al database

        //    }



        //    return View("Index");
        //}




        [Authorize(Roles = "Admin")]
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
