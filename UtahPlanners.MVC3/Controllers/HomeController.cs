using System.Web.Mvc;
using UtahPlanners.MVC3.Extensions;
using UtahPlanners.MVC3.Services;
using UtahPlanners.MVC3.Models.Home;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UtahPlanners.MVC3.Controllers
{
    public class HomeController : Controller
    {
        private IServiceFactory _factory;

        public HomeController(IServiceFactory factory)
        {
            _factory = factory;
        }

        public ActionResult Default()
        {
            var client = _factory.CreateService();
            var prop = client.GetShowcaseProperty();
            return View(prop);
        }

        [HttpGet]
        public ActionResult Index()
        {
            //Get index table rows, including the calculated overall score
            var client = _factory.CreateService();
            var indecies = client.SafeExecution(c => c.GetAllIndecies()).ToList();
            
            var client2 = _factory.CreateService();
            var lookupValues = client2.SafeExecution(c => c.GetLookupValues());
            
            var model = new IndexModel
            {
                Records = Convert(indecies),
                Filter = null,
                Sort = null,
                DropDowns = Convert(lookupValues)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IndexModel model)
        {
            //Get filtered index records
            var client = _factory.CreateService();
            
            var filter = Convert(model.Filter);
            var sort = Convert(model.Sort);
            
            var indicies = client.SafeExecution(c => c.GetIndecies(filter, sort)).ToList();

            var client2 = _factory.CreateService();
            var lookupValues = client2.SafeExecution(c => c.GetLookupValues());
            model.DropDowns = Convert(lookupValues);

            model.Records = Convert(indicies);
            return View(model);
        }

        public ActionResult Property(int id)
        {
            //Get the details for a property
            var client = _factory.CreateService();
            var property = client.SafeExecution(c => c.GetProperty(id));
            var prop = Convert(property);
            return View(prop);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult GetPicture(int id)
        {
            var client = _factory.CreateService();
            var picture = client.GetPicture(id);
            return File(picture.binaryData, "image/png");
        }

        #region Mapping Methods
        
        private Property Convert(PropertyService.Property p)
        {
            return new Property
            {
                Id = p.id,
                Address = Convert(p.Address),
                PictureIds = p.PictureIds.ToList(),
                SecondaryPictureId = p.SecondaryPictureId,
                PropertyType = p.PropertyType.description,
                Score = p.Score,
                StreetSafety = p.StreetSafteyCode.description,
                BuildingEnclosure = p.EnclosureCode.description,
                CommonAreas = p.CommonCode.description,
                StreetConnectivity = p.StreetconnCode.description,
                StreetWalkability = p.StreetwalkCode.description,
                Walkscore = p.walkscore.HasValue ? p.walkscore.Value : 0,
                NeighborhoodCondition = p.NeighborhoodCode.description,
                TwoFiftySingleFamily = p.twoFiftySingleFam.HasValue ? p.twoFiftySingleFam.Value : 0,
                TwoFiftyApartments = p.twoFiftyApts.HasValue ? p.twoFiftyApts.Value : 0,
                Density = p.density.HasValue ? p.density.Value : 0,
                Area = p.area.HasValue ? p.area.Value : 0,
                Units = p.units.HasValue ? p.units.Value : 0,
                StreetType = p.StreetType.description,
                YearBuilt = p.yearBuilt.HasValue ? p.yearBuilt.Value : 0,
                SocioEcon = p.SocioEconCode.description,
                Notes = p.notes
            };
        }

        private Address Convert(PropertyService.Address a)
        {
            return new Address
            {
                Street = a.street1,
                City = a.city,
                State = a.state,
                ZipCode = a.zip
            };
        }

        private List<Index> Convert(List<PropertyService.PropertiesIndex> indecies)
        {
            var indexList = new List<Index>();
            indecies.ForEach(i =>
            {
                if (i != null) 
                    indexList.Add(Convert(i));
            });
            return indexList;
        }

        private Index Convert(PropertyService.PropertiesIndex index)
        {
            return new Index
            {
                Id = index.id,
                City = index.city,
                Score = index.OverallScore.HasValue ? index.OverallScore.Value : 0,
                PropertyType = index.PropertyTypeDescription,
                Density = index.density.HasValue ? index.density.Value : 0,
                Units = index.units.HasValue ? index.units.Value : 0,
                YearBuilt = index.yearBuilt.HasValue ? index.yearBuilt.ToString() : String.Empty,
                StreetType = index.StreetTypeDescription,
                StreetWalkDescription = index.StreetWalkDescription,
                Walkscore = index.walkscore.HasValue ? index.walkscore.Value : 0,
                SocioEconDescription = index.SocioEconDescription,
                TwoFiftySingleFamily = index.twoFiftySingleFam.HasValue ? index.twoFiftySingleFam.Value : 0
            };
        }

        private PropertyService.IndexFilter Convert(IndexFilter f)
        {
            if (f != null)
            {
                return new PropertyService.IndexFilter
                {
                    PropertyId = f.PropertyId,
                    City = f.City,
                    PropertyType = f.PropertyType,
                    DensityRange = Convert(f.DensityRange),
                    AreaRange = Convert(f.AreaRange),
                    UnitRange = Convert(f.UnitRange),
                    StreetType = f.StreetType,
                    YearBuiltRange = Convert(f.YearBuiltRange),
                    SocioEconType = f.SocioEconType,
                    ScoreRange = Convert(f.ScoreRange),
                    StreetSafetyType = f.StreetSafetyType,
                    BuildingEnclosureType = f.BuildingEnclosureType,
                    CommonAreasType = f.CommonAreasType,
                    StreetConnectivityType = f.StreetConnectivityType,
                    StreetWalkabilityType = f.StreetWalkabilityType,
                    WalkscoreRange = Convert(f.WalkscoreRange),
                    NeighborhoodConditionType = f.NeighborhoodConditionType,
                    TwoFiftySingleFamilyRange = Convert(f.TwoFiftySingleFamilyRange),
                    TwoFiftyApartmentsRange = Convert(f.TwoFiftyApartmentsRange)
                };
            }
            return null;
        }

        private PropertyService.RangeOfNullableOfdouble5F2dSckg Convert(Range<double?> range)
        {
            if (range != null)
            {
                var serviceRange = new PropertyService.RangeOfNullableOfdouble5F2dSckg();
                serviceRange.LowValue = range.LowValue;
                serviceRange.HighValue = range.HighValue;
                return serviceRange;
            }
            return null;
        }

        private PropertyService.RangeOfNullableOfint5F2dSckg Convert(Range<int?> range)
        {
            if (range != null)
            {
                var serviceRange = new PropertyService.RangeOfNullableOfint5F2dSckg();
                serviceRange.LowValue = range.LowValue;
                serviceRange.HighValue = range.HighValue;
                return serviceRange;
            }
            return null;
        }

        private PropertyService.IndexSort Convert(IndexSort sort)
        {
            if (sort != null)
            {
                return new PropertyService.IndexSort
                {
                    Column = Convert(sort.Column),
                    Direction = Convert(sort.Direction)
                };
            }
            return null;
        }
        
        private PropertyService.IndexColumn Convert(IndexColumn? c)
        {
            var column = PropertyService.IndexColumn.Id; // Default
            if (c.HasValue)
            {
                switch (c)
                {
                    case IndexColumn.City:
                        column = PropertyService.IndexColumn.City;
                        break;
                    case IndexColumn.Id:
                        column = PropertyService.IndexColumn.Id;
                        break;
                    case IndexColumn.Score:
                        column = PropertyService.IndexColumn.Score;
                        break;
                    case IndexColumn.Type:
                        column = PropertyService.IndexColumn.Type;
                        break;
                    case IndexColumn.Density:
                        column = PropertyService.IndexColumn.Density;
                        break;
                    case IndexColumn.Units:
                        column = PropertyService.IndexColumn.Units;
                        break;
                    case IndexColumn.Year:
                        column = PropertyService.IndexColumn.Year;
                        break;
                    case IndexColumn.StreetType:
                        column = PropertyService.IndexColumn.StreetType;
                        break;
                    case IndexColumn.Walkability:
                        column = PropertyService.IndexColumn.Walkability;
                        break;
                    case IndexColumn.Walkscore:
                        column = PropertyService.IndexColumn.Walkscore;
                        break;
                    case IndexColumn.SocioEcon:
                        column = PropertyService.IndexColumn.SocioEcon;
                        break;
                    case IndexColumn.TwoFiftySF:
                        column = PropertyService.IndexColumn.TwoFiftySF;
                        break;
                }
            }
            return column;
        }

        private PropertyService.Direction Convert(Direction? d)
        {
            var direction = PropertyService.Direction.Ascending; // Default
            if (d.HasValue)
            {
                switch (d)
                {
                    case Direction.Ascending:
                        direction = PropertyService.Direction.Ascending;
                        break;
                    case Direction.Descending:
                        direction = PropertyService.Direction.Descending;
                        break;
                }
            }
            return direction;
        }

        private DropDowns Convert(PropertyService.LookupValues lv)
        {
            if (lv != null)
            {
                return new DropDowns
                {
                    PropertyTypes = Convert(lv.PropertyTypes),
                    StreetTypes = Convert(lv.StreetTypes),
                    SocioEconCodes = Convert(lv.SocioEconCodes),
                    StreetSafetyCodes = Convert(lv.StreetSafetyCodes),
                    EnclosureCodes = Convert(lv.EnclosureCodes),
                    CommonCodes = Convert(lv.CommonCodes),
                    StreetconnCodes = Convert(lv.StreetconnCodes),
                    StreetwalkCodes = Convert(lv.StreetwalkCodes),
                    NeighborhoodCodes = Convert(lv.NeighborhoodCodes)
                };
            }
            return null;
        }

        private SelectList Convert<T>(T[] items)
        {
            return new SelectList(items.ToList(), "id", "description");
        }

        #endregion
    }
}
