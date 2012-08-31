using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.Domain;
using UtahPlanners.Infrastructure;

namespace UtahPlanners.MVC.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private const string MainImageUrl = "../Content/images/test.jpg";

        public ActionResult Default()
        {
            ViewData["MainImageUrl"] = HomeController.MainImageUrl;

            return View();
        }

        public ActionResult Index()
        {
            //PropertyRepository repo = new PropertyRepository();
            //var properties = repo.GetAllProperties().ToList();

            //return View(properties);
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
