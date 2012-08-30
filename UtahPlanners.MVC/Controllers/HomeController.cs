using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.Repository;

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
            PropertyRepository repo = new PropertyRepository();
            List<PropertyExtensions> properties = repo.GetAllProperties().ToList<PropertyExtensions>(); ;

            return View(properties);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
