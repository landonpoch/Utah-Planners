using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.POC.Domain;
using UtahPlanners.POC.Repository;
using UtahPlanners.POC.Repository.Mappings;

namespace UtahPlanners.POC.Controllers
{
    public class HomeController : Controller
    {
        private IPropertyRepository _repo;

        public HomeController()
        {
            //_repo = new MongoPropertyRepository(new MongoClient().GetServer().GetDatabase("POC"));
            _repo = new EfPropertyRepository(new PropertyContext());
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var property = new Property("some name");
            var id = _repo.Add(property);
            var result = _repo.GetAllProperties();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
