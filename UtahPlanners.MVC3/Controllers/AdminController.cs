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
    [Authorize]
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

            if (client.SafeExecution(c => c.ValidateUser(model.Username, model.Password)))
            {
                // TODO: Set auth cookie with username
                RedirectToAction("Admin", "Admin");
            }
            
            // TODO: Bad username/password
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
            // TODO: Messaging for account creation scenarios

            return View();
        }

        [Authorize]
        public ActionResult Admin()
        {
            return View();
        }
    }
}
