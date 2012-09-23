using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.MVC3.Services;
using UtahPlanners.MVC3.Extensions;
using UtahPlanners.MVC3.Models.Admin;

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

        public ActionResult Property(int? id)
        {
            var client = _factory.CreatePropertyService();
            var lookupValues = client.SafeExecution(c => c.GetLookupValues());
            var model = new AdminProperty
            {
                LookupValues = lookupValues
            };

            if (id.HasValue)
            {
                ViewBag.Title = "Edit Property";
                ViewBag.NewPictureTitle = "Add New Pictures:";

                client = _factory.CreatePropertyService();
                var property = client.SafeExecution(c => c.GetProperty(id.Value));

                model.Property = property;
                model.PropertyType = property.PropertyType.id.ToString();
                model.StreetType = property.StreetType.id.ToString();
                model.SocioEcon = property.SocioEconCode.id.ToString();
                model.StreetSafety = property.StreetSafteyCode.id.ToString();
                model.BuildingEnclosure = property.EnclosureCode.id.ToString();
                model.CommonAreas = property.CommonCode.id.ToString();
                model.StreetConnectivity = property.StreetconnCode.id.ToString();
                model.StreetWalkability = property.StreetwalkCode.id.ToString();
                model.NeighborhoodCondition = property.NeighborhoodCode.id.ToString();
            }
            else
            {
                ViewBag.Title = "Create New Property";
                ViewBag.NewPictureTitle = "Pictures:";
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Property(AdminProperty model)
        {
            var client = _factory.CreatePropertyService();
            //var result = client.SafeExecution(c => c.SaveProperty());

            return View(model);
        }

        public ActionResult PropertyList()
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
