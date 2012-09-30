using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.MVC3.Services;
using UtahPlanners.MVC3.Extensions;
using UtahPlanners.MVC3.Models.Admin;
using UtahPlanners.MVC3.Models.Home;

namespace UtahPlanners.MVC3.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        private const string PostMessageKey = "PostMessage";

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
                ViewBag.SubmitText = "Submit Changes";

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
                ViewBag.SubmitText = "Create New Property";
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Property(AdminProperty model)
        {
            var prop = SetCodes(model);
            var client = _factory.CreatePropertyService();
            var id = client.SafeExecution(c => c.SaveProperty(model.Property));

            // TODO: Figure out success/fail messaging and behavior
            return RedirectToAction("Property", new { id });
        }

        public ActionResult PropertyList()
        {
            var client = _factory.CreatePropertyService();
            var props = client.SafeExecution(c => c.GetAllProperties());

            return View(props.ToList());
        }

        public ActionResult PropertyGrid(string sort)
        {
            var client = _factory.CreatePropertyService();
            var props = client.SafeExecution(c => c.GetAllProperties().ToList());

            

            return View(props);
        }

        [HttpPost]
        public ActionResult DeleteProperty(int id)
        {
            var client = _factory.CreatePropertyService();
            bool success = client.SafeExecution(c => c.DeleteProperty(id));

            TempData[PostMessageKey] = success ? "Property Successfully Deleted." : "An error occurred while trying to delete the property.";
            return RedirectToAction("PropertyList"); // Always redirect on posts to avoid the resend post-data browser message
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

        private PropertyService.Property SetCodes(AdminProperty model)
        {
            var property = model.Property;

            // Map new drop down selections
            property.typeCode = Int32.Parse(model.PropertyType);
            property.streetCode = Int32.Parse(model.StreetType);
            property.socioEcon = Int32.Parse(model.SocioEcon);
            property.streetSaftey = Int32.Parse(model.StreetSafety);
            property.buildingEnclosure = Int32.Parse(model.BuildingEnclosure);
            property.commonAreas = Int32.Parse(model.CommonAreas);
            property.streetConn = Int32.Parse(model.StreetConnectivity);
            property.streetWalk = Int32.Parse(model.StreetWalkability);
            property.neighCondition = Int32.Parse(model.NeighborhoodCondition);
            
            return property;
        }
    }
}
