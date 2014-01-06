using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.POC.Domain;
using UtahPlanners.POC.Models;
using UtahPlanners.POC.Repository;
using UtahPlanners.POC.Repository.Mappings;

namespace UtahPlanners.POC.Controllers
{
    public class HomeController : Controller
    {
        private IPropertyRepository _repo;
        private ILookupRepository<PropertyType> _lookupRepo;

        public HomeController()
        {
            var db = new MongoClient().GetServer().GetDatabase("POC");
            _repo = new MongoPropertyRepository(db);
            _lookupRepo = new MongoLookupRepository<PropertyType>(db);
            //_repo = new EfPropertyRepository(new PropertyContext());
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var propertyTypes = _lookupRepo.GetAllLookupValues();
            var lookupValues = propertyTypes.Select(pt => new LookupValueModel { Id = pt.Id, Description = pt.Description }).ToList();
            return View(new IndexModel { PropertyTypes = lookupValues });
        }

        [HttpPost]
        public ActionResult UpdateLookupValue(LookupValueModel lookupValue)
        {
            var result = _lookupRepo.UpdateLookupValue(lookupValue.Id, lookupValue.Description);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreatePropertyType(string description)
        {
            var propertyType = new PropertyType(description);
            var result = _lookupRepo.CreateLookupValue(propertyType);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeletePropertyType(Guid id)
        {
            var result = _lookupRepo.DeleteLookupValue(id);
            return RedirectToAction("Index");
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
