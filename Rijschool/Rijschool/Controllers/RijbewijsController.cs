using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rijschool.Models;

namespace Rijschool.Controllers
{
    [Authorize(Roles="Personeel")]
    public class RijbewijsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rijbewijs
        public ActionResult Index()
        {
            return View(db.TypeRijbewijzen.ToList());
        }

        // GET: Rijbewijs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rijbewijs rijbewijs = db.TypeRijbewijzen.Find(id);
            if (rijbewijs == null)
            {
                return HttpNotFound();
            }
            return View(rijbewijs);
        }

        // GET: Rijbewijs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rijbewijs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naam")] Rijbewijs rijbewijs)
        {
            if (ModelState.IsValid)
            {
                db.TypeRijbewijzen.Add(rijbewijs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rijbewijs);
        }

        // GET: Rijbewijs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rijbewijs rijbewijs = db.TypeRijbewijzen.Find(id);
            if (rijbewijs == null)
            {
                return HttpNotFound();
            }
            return View(rijbewijs);
        }

        // POST: Rijbewijs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam")] Rijbewijs rijbewijs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rijbewijs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rijbewijs);
        }

        // GET: Rijbewijs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rijbewijs rijbewijs = db.TypeRijbewijzen.Find(id);
            if (rijbewijs == null)
            {
                return HttpNotFound();
            }
            return View(rijbewijs);
        }

        // POST: Rijbewijs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rijbewijs rijbewijs = db.TypeRijbewijzen.Find(id);
            db.TypeRijbewijzen.Remove(rijbewijs);
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
