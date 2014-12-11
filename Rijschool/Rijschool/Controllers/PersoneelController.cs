using Microsoft.AspNet.Identity;
using Rijschool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Rijschool.Controllers
{
    [Authorize(Roles="Admin")]
    public class PersoneelController : AccountController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //GET: Personeel
        public ActionResult Index(int? pageKlant, int? pageInstructeur)
        {
            PersoneelDashboardVM dash = new PersoneelDashboardVM();
            dash.AantalKlanten = db.Klanten.Count();
            dash.AantalInstructeurs = db.Instructeurs.Count();

            int pageSize = 10;
            dash.PageKlant = (pageKlant ?? 1);
            dash.PageInstructeur = (pageInstructeur ?? 1);

            var klanten = db.Klanten.ToList();
            dash.Klanten = klanten.ToPagedList(dash.PageKlant, pageSize);
            var instructeurs = db.Instructeurs.ToList();
            dash.Instructeurs = instructeurs.ToPagedList(dash.PageInstructeur, pageSize);
            
            return View(dash);
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
                var user = new Personeel
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
                    var roleResult = UserManager.AddToRole(user.Id, "Personeel");
                    if (roleResult.Succeeded) {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Home");
                    }
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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