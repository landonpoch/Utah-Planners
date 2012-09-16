using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.MVC3.Services;
using UtahPlanners.MVC3.Extensions;

namespace UtahPlanners.MVC3.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        IServiceFactory _factory;
        
        public AdminController(IServiceFactory factory)
        {
            _factory = factory;
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult CreateCodes()
        {
            return View();
        }

        public ActionResult ReadCodes()
        {
            return View();
        }

        public ActionResult CreateTypes()
        {
            return View();
        }

        public ActionResult ReadTypes()
        {
            return View();
        }

        public ActionResult Weights()
        {
            return View();
        }
    }
}
