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

        #region Property Maintenance
        
        public ActionResult Property(int? id)
        {
            using (var wcf = _factory.CreatePropertyServiceWrapper())
            {
                var lookupValues = wcf.Client.GetAllLookupValues();
                var model = new AdminProperty
                {
                    LookupValues = lookupValues
                };

                if (id.HasValue)
                {
                    ViewBag.Title = "Edit Property";
                    ViewBag.NewPictureTitle = "Add New Pictures:";
                    ViewBag.SubmitText = "Submit Changes";

                    var property = wcf.Client.FindAdminProperty(id.Value);
                    model.Property = property;
                    model.PropertyType = property.Type.ToString();
                    model.StreetType = property.StreetType.ToString();
                    model.SocioEcon = property.SocioEcon.ToString();
                    model.StreetSafety = property.StreetSafety.ToString();
                    model.BuildingEnclosure = property.BuildingEnclosure.ToString();
                    model.CommonAreas = property.CommonAreas.ToString();
                    model.StreetConnectivity = property.StreetConnectivity.ToString();
                    model.StreetWalkability = property.StreetWalkability.ToString();
                    model.NeighborhoodCondition = property.NeighborhoodCondition.ToString();
                }
                else
                {
                    ViewBag.Title = "Create New Property";
                    ViewBag.NewPictureTitle = "Pictures:";
                    ViewBag.SubmitText = "Create New Property";
                }

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Property(AdminProperty model)
        {
            var prop = SetCodes(model);
            using (var wcf = _factory.CreatePropertyServiceWrapper())
            {
                var property = Convert(model.Property);
                var id = wcf.Client.SaveProperty(property);

                // TODO: Figure out success/fail messaging and behavior
                return RedirectToAction("Property", new { id });
            }
        }

        public ActionResult PropertyList()
        {
            using (var wcf = _factory.CreatePropertyServiceWrapper())
            {
                var props = wcf.Client.FindAllAdminIndecies(null);

                var model = new PropertyGrid
                {
                    Properties = props.ToList(),
                    SortOptions = GetDefaultSortOptions()
                };

                return View(model);
            }
        }

        public ActionResult PropertyGrid(string sort)
        {
            PropertyService.PropertySort propSort = Convert(sort);

            using (var wcf = _factory.CreatePropertyServiceWrapper())
            {
                var props = wcf.Client.FindAllAdminIndecies(propSort);

                var model = new PropertyGrid
                {
                    Properties = props.ToList(),
                    SortOptions = GetSortOptions(sort)
                };

                return PartialView("_PropertyGrid", model);
            }
        }

        [HttpPost]
        public ActionResult DeleteProperty(int id)
        {
            using (var wcf = _factory.CreatePropertyServiceWrapper())
            {
                bool success = wcf.Client.DeleteProperty(id);

                TempData[PostMessageKey] = success ? "Property Successfully Deleted." : "An error occurred while trying to delete the property.";
                return RedirectToAction("PropertyList"); // Always redirect on posts to avoid the resend post-data browser message
            }
        }

        #endregion

        #region LookupTypes

        public ActionResult CreateTypes()
        {
            return View(new CreateTypeModel());
        }

        [HttpPost]
        public ActionResult CreateTypes(CreateTypeModel model)
        {
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                var lookupType = (PropertyService.LookupType)model.SelectedType;
                bool result = client.CreateLookupType(lookupType, model.TypeDescription);
            }
            return RedirectToAction("CreateTypes");
        }

        public ActionResult ReadTypes()
        {
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                var model = new ReadTypeModel
                {
                    SelectedType = 0,
                    KeyValuePairs = client.GetLookupTypes(PropertyService.LookupType.PropertyType)
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ReadTypes(ReadTypeModel model)
        {
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                model.KeyValuePairs = client.GetLookupTypes((PropertyService.LookupType)model.SelectedType);
                return View(model);
            }
        }

        [HttpPost]
        public string ModifyType(ReadTypeModel model)
        {
            var result = "An error occured while submitting your request"; // Default response
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                if (model.SelectedId.HasValue)
                {
                    PropertyService.LookupType lookupType = (PropertyService.LookupType)model.SelectedType;
                    bool wasSuccessful = client.ModifyLookupType(lookupType, model.SelectedId.Value, model.KeyValuePairs[model.SelectedId.Value]);
                    if (wasSuccessful) // Successful response
                        result = "Successfully submitted your request";
                }
            }
            return result;
        }

        [HttpPost]
        public string DeleteType(ReadTypeModel model)
        {
            var result = "An error occured while deleting the type";
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                if (model.SelectedId.HasValue)
                {
                    PropertyService.LookupType lookupType = (PropertyService.LookupType)model.SelectedType;
                    bool wasSuccessful = client.DeleteLookupType(lookupType, model.SelectedId.Value);
                    if (wasSuccessful)
                        result = "Successfully deleted the type";
                }
            }
            return result;
        }

        #endregion

        #region LookupCodes
        
        public ActionResult CreateCodes()
        {
            return View(new CreateCodeModel());
        }

        [HttpPost]
        public ActionResult CreateCodes(CreateCodeModel model)
        {
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                var lookupCode = (PropertyService.LookupCode)model.SelectedCode;
                bool result = client.CreateLookupCode(lookupCode, model.CodeDescription, model.Weight.Value);
            }
            return RedirectToAction("CreateCodes");
        }

        public ActionResult ReadCodes()
        {
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                var result = Convert(client.GetLookupCodes(PropertyService.LookupCode.CommonCode));
                var model = new ReadCodeModel
                {
                    SelectedCode = 0,
                    CodeData = result
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ReadCodes(ReadCodeModel model)
        {
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                model.CodeData = Convert(client.GetLookupCodes((PropertyService.LookupCode)model.SelectedCode));
                return View(model);
            }
        }

        [HttpPost]
        public string ModifyCode(ReadCodeModel model)
        {
            var result = "An error occured while submitting your request"; // Default response
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                if (model.SelectedId.HasValue)
                {
                    PropertyService.LookupCode lookupCode = (PropertyService.LookupCode)model.SelectedCode;
                    bool wasSuccessful = client.ModifyLookupCode(
                        lookupCode, 
                        model.SelectedId.Value, 
                        model.CodeData[model.SelectedId.Value].Description,
                        model.CodeData[model.SelectedId.Value].Weight
                    );
                    if (wasSuccessful) // Successful response
                        result = "Successfully submitted your request";
                }
            }
            return result;
        }

        [HttpPost]
        public string DeleteCode(ReadCodeModel model)
        {
            var result = "An error occured while deleting the code";
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                if (model.SelectedId.HasValue)
                {
                    PropertyService.LookupCode lookupCode = (PropertyService.LookupCode)model.SelectedCode;
                    bool wasSuccessful = client.DeleteLookupCode(lookupCode, model.SelectedId.Value);
                    if (wasSuccessful)
                        result = "Successfully deleted the code";
                }
            }
            return result;
        }

        #endregion

        #region Weights
        
        public ActionResult Weights()
        {
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                var weights = client.GetWeights();
                return View(weights);
            }
        }

        [HttpPost]
        public string Weights(PropertyService.Weight model)
        {
            var result = "Could not update the weights at this time";
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                if (client.UpdateWeights(model))
                    result = "Successfully updated weights";
                return result;
            }
        }

        #endregion

        #region CSV File

        public void Csv()
        {
            var sort = new PropertyService.PropertySort { Column = PropertyService.PropertyColumn.Id, Direction = PropertyService.Direction.Ascending };
            var properties = new List<UtahPlanners.MVC3.PropertyService.CsvPropertyDTO>();
            using (var wcf = _factory.CreatePropertyServiceWrapper())
            {
                properties = wcf.Client.FindAllCsvProperties().ToList();
            }

            Response.ContentType = "application/x-download";
            Response.AddHeader("Content-Disposition", "attachment;filename=properties.csv");

            Response.Write("ID,Prop_Type,Street1,Street2,City,State,Zip,Country," +
                "Density,Area,Units,Street_Type,Year_Built,Socio_Econ," +
                "Street_Safety,Building_Enclosure,Common_Areas,Street_Connectivity," +
                "Street_Walkability,Walkscore,Neighborhood_Condition,250_SF,250_Apts" + Environment.NewLine);
            var format = "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}," +
                "{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}," + 
                "{20},{21},{22}" + Environment.NewLine;
            foreach (var prop in properties)
            {
                Response.Write(String.Format(format, 
                    prop.Id,
                    prop.PropertyType,
                    prop.Address.Street1,
                    prop.Address.Street2,
                    prop.Address.City,
                    prop.Address.State,
                    prop.Address.Zip,
                    prop.Address.Country,
                    prop.Density,
                    prop.Area,
                    prop.Units,
                    prop.StreetType,
                    prop.YearBuilt,
                    prop.SocioEconType,
                    prop.StreetSafetyCode,
                    prop.BuildingEnclosureCode,
                    prop.CommonAreasCode,
                    prop.StreetConnectivityCode,
                    prop.StreetWalkabilityCode,
                    prop.Walkscore,
                    prop.NeighborhoodConditionCode,
                    prop.TwoFiftySingleFamily,
                    prop.TwoFiftyAppartments));
            }

        }

        #endregion

        #region Private Methods

        private Dictionary<int, string> GetDefaultSortOptions()
        {
            return new Dictionary<int, string>
            {
                { 1, "1d" },
                { 2, "2a" },
                { 3, "3a" },
                { 4, "4a" },
                { 5, "5a" },
                { 6, "6a" },
                { 7, "7a" },
                { 8, "8a" }
            };
        }

        private Dictionary<int, string> GetSortOptions(string sort)
        {
            int column = Int32.Parse(sort[0].ToString());
            string direction = sort[1].ToString();
            var sortOptions = GetDefaultSortOptions();
            sortOptions[1] = "1a";
            string nextDirection = direction == "a" ? "d" : "a";
            sortOptions[column] = column.ToString() + nextDirection;
            return sortOptions;
        }

        private PropertyService.Property SetCodes(AdminProperty model)
        {
            var property = Convert(model.Property);

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

        private PropertyService.PropertySort Convert(string sort)
        {
            if (String.IsNullOrEmpty(sort))
                return null;

            return new PropertyService.PropertySort
            {
                Column = ConvertColumn(sort[0]),
                Direction = ConvertDirection(sort[1])
            };
        }

        private PropertyService.PropertyColumn ConvertColumn(char column)
        {
            var result = PropertyService.PropertyColumn.Id; // Default

            if (column == '1') result = PropertyService.PropertyColumn.Id;
            if (column == '2') result = PropertyService.PropertyColumn.City;
            if (column == '3') result = PropertyService.PropertyColumn.Description;
            if (column == '4') result = PropertyService.PropertyColumn.Density;
            if (column == '5') result = PropertyService.PropertyColumn.Units;
            if (column == '6') result = PropertyService.PropertyColumn.YearBuilt;
            if (column == '7') result = PropertyService.PropertyColumn.AdminNotes;
            if (column == '8') result = PropertyService.PropertyColumn.NotFinished;

            return result;
        }

        private PropertyService.Direction ConvertDirection(char direction)
        {
            var result = PropertyService.Direction.Ascending; // Default

            if (direction == 'a') result = PropertyService.Direction.Ascending;
            if (direction == 'd') result = PropertyService.Direction.Descending;

            return result;
        }

        private PropertyService.Property Convert(PropertyService.PropertyDTO dto)
        {
            return new PropertyService.Property
            {
                Address = Convert(dto.Address)
            };
        }

        private PropertyService.Address Convert(PropertyService.AddressDTO dto)
        {
            return new PropertyService.Address
            {
                street1 = dto.Street1,
                street2 = dto.Street2,
                city = dto.City,
                state = dto.State,
                zip = dto.Zip,
                country = dto.Country,
            };
        }

        private Dictionary<int, CodeDetails> Convert(Dictionary<int, Tuple<string, int>> codes)
        {
            return codes.ToDictionary(c => c.Key, c => new CodeDetails { Description = c.Value.Item1, Weight = c.Value.Item2 });
        }

        #endregion
    }
}
