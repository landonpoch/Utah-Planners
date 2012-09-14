using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.MVC3.Models.Admin;
using UtahPlanners.MVC3.Services;
using UtahPlanners.MVC3.Extensions;

namespace UtahPlanners.MVC3.Controllers
{
    public class AdminController : Controller
    {
        IServiceFactory _factory;
        
        public AdminController(IServiceFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            var client = _factory.CreateMembershipService();

            if (ModelState.IsValid)
            {
                if (client.SafeExecution(c => c.ValidateUser(model.Username, model.Password)))
                {
                    var authService = _factory.CreateFormsAuthenticationService();
                    authService.SignIn(model.Username, true);
                    return RedirectToAction("Admin", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and password combination.");
                }
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(CreateAccount model)
        {
            var client = _factory.CreateMembershipService();
            var status = client.CreateUser(model.Username, model.Password, model.Email);

            if (status == MembershipService.MembershipStatus.Success)
            {
                var authService = _factory.CreateFormsAuthenticationService();
                authService.SignIn(model.Username, true);
                return RedirectToAction("Admin", "Admin");
            }

            return View();
        }

        public ActionResult Logout()
        {
            var formsAuthService = _factory.CreateFormsAuthenticationService();
            formsAuthService.SignOut();
            return RedirectToAction("Default", "Home");
        }

        [Authorize]
        public ActionResult Admin()
        {
            return View();
        }
    }
}
