using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyWorkDemo.Data;
using MyWorkDemo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyWorkDemo.Controllers
{
    public class AccountController : Controller
    {
        private MyWorkDbContext db;

        public AccountController(MyWorkDbContext db)
        {
            this.db = db;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Username = model.Username;
                user.Password = model.Password;

                db.User.Add(user);
                db.SaveChanges();

                ViewData["message"] = "User created successfully!";
            }
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model, string returnUrl)
        {
            bool isUservalid = false;

            User user = db.User.Where(usr =>
            usr.Username == model.Username &&
            usr.Password == model.Password).SingleOrDefault();

            if (user != null)
            {
                isUservalid = true;
            }


            if (ModelState.IsValid && isUservalid)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, user.Username));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();
                props.IsPersistent = model.RememberMe;

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, props).Wait();

                return RedirectToAction("Index", "Activites");
            }
            else
            {
                ViewData["message"] = "Invalid UserName or Password!";
            }

            return View();
        }



        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


    }
}
