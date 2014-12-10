using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rijschool.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Rijschool.Controllers
{
    [Authorize(Roles="Personeel")]
    public class InstructeurController : AccountController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructeur
        public ActionResult Index()
        {
            return View(db.Instructeurs.ToList());
        }

        //
        // GET: /Personeel/Registreer
        public ActionResult Registreer()
        {
            return View();
        }

        //
        // POST: /Personeel/Registreer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registreer(RegistreerVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new Instructeur
                {
                    Voornaam = model.Voornaam,
                    Familienaam = model.Familienaam,
                    Adres = model.Familienaam,
                    Gemeente = model.Gemeente,
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Wachtwoord);
                if (result.Succeeded)
                {
                    var roleResult = UserManager.AddToRole(user.Id, "Instructeur");
                    if (roleResult.Succeeded)
                    {
                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Personeel");
                    }
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: Instructeur/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructeur instructeur = db.Instructeurs.Find(id);
            if (instructeur == null)
            {
                return HttpNotFound();
            }
            return View(instructeur);
        }

        // GET: Instructeur/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instructeur/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Familienaam,Voornaam,Adres,Gemeente")] Instructeur instructeur)
        {
            if (ModelState.IsValid)
            {
                db.Instructeurs.Add(instructeur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instructeur);
        }

        // GET: Instructeur/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructeur instructeur = db.Instructeurs.Find(id);
            if (instructeur == null)
            {
                return HttpNotFound();
            }
            return View(instructeur);
        }

        // POST: Instructeur/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Familienaam,Voornaam,Adres,Gemeente")] Instructeur instructeur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructeur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instructeur);
        }

        // GET: Instructeur/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructeur instructeur = db.Instructeurs.Find(id);
            if (instructeur == null)
            {
                return HttpNotFound();
            }
            return View(instructeur);
        }

        // POST: Instructeur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Instructeur instructeur = db.Instructeurs.Find(id);
            db.Instructeurs.Remove(instructeur);
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
