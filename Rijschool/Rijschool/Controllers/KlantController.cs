using Microsoft.AspNet.Identity;
using Rijschool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rijschool.Controllers
{
    public class KlantController : AccountController
    {
        
        // GET: Klant
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Registreer()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registreer(KlantVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new Klant { 
                    Voornaam = model.Voornaam,
                    Familienaam = model.Familienaam,
                    Adres = model.Familienaam,
                    Gemeente = model.Gemeente,
                    Email = model.Email,
                    UserName = model.Email,
                    KlantSedert = DateTime.Now
                };
                var result = await UserManager.CreateAsync(user, model.Wachtwoord);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
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