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
                    PropertyTypes = GetDropDownList(lv.PropertyTypes, Convert),
                    StreetTypes = GetDropDownList(lv.StreetTypes, Convert),
                    SocioEconCodes = GetDropDownList(lv.SocioEconCodes, Convert),
                    StreetSafetyCodes = GetDropDownList(lv.StreetSafetyCodes, Convert),
                    EnclosureCodes = GetDropDownList(lv.EnclosureCodes, Convert),
                    CommonCodes = GetDropDownList(lv.CommonCodes, Convert),
                    StreetconnCodes = GetDropDownList(lv.StreetconnCodes, Convert),
                    StreetwalkCodes = GetDropDownList(lv.StreetwalkCodes, Convert),
                    NeighborhoodCodes = GetDropDownList(lv.NeighborhoodCodes, Convert)
                };
            }
            return null;
        }

        private List<DropDownItem> GetDropDownList<T>(T[] lookupValues, Func<T, DropDownItem> convert)
        {
            var items = new List<DropDownItem>();
            items.Add(new DropDownItem { Id = String.Empty, Value = String.Empty });
            lookupValues.ToList().ForEach(lv =>
            {
                items.Add(convert(lv));
            });
            return items;
        }

        private DropDownItem Convert(PropertyService.PropertyType propertyType)
        {
            return new DropDownItem
            {
                Id = propertyType.id.ToString(),
                Value = propertyType.description
            };
        }

        private DropDownItem Convert(PropertyService.StreetType streetType)
        {
            return new DropDownItem
            {
                Id = streetType.id.ToString(),
                Value = streetType.description
            };
        }

        private DropDownItem Convert(PropertyService.SocioEconCode socioEconCode)
        {
            return new DropDownItem
            {
                Id = socioEconCode.id.ToString(),
                Value = socioEconCode.description
            };
        }

        private DropDownItem Convert(PropertyService.StreetSafteyCode streetSafetyCode)
        {
            return new DropDownItem
            {
                Id = streetSafetyCode.id.ToString(),
                Value = streetSafetyCode.description
            };
        }

        private DropDownItem Convert(PropertyService.EnclosureCode enclosureCode)
        {
            return new DropDownItem
            {
                Id = enclosureCode.id.ToString(),
                Value = enclosureCode.description
            };
        }

        private DropDownItem Convert(PropertyService.CommonCode commonCode)
        {
            return new DropDownItem
            {
                Id = commonCode.id.ToString(),
                Value = commonCode.description
            };
        }

        private DropDownItem Convert(PropertyService.StreetconnCode streetconnCode)
        {
            return new DropDownItem
            {
                Id = streetconnCode.id.ToString(),
                Value = streetconnCode.description
            };
        }

        private DropDownItem Convert(PropertyService.StreetwalkCode streetwalkCode)
        {
            return new DropDownItem
            {
                Id = streetwalkCode.id.ToString(),
                Value = streetwalkCode.description
            };
        }

        private DropDownItem Convert(PropertyService.NeighborhoodCode neighborhoodCode)
        {
            return new DropDownItem
            {
                Id = neighborhoodCode.id.ToString(),
                Value = neighborhoodCode.description
            };
        }

        #endregion
    }
}
