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
        public ActionResult Index(string proptype, string density, string walkscore)
        {
            var client = _factory.CreateService();
            var lookupValues = client.SafeExecution(c => c.GetLookupValues());
            PropertyService.IndexFilter filter = GetFilterFromQueryString(proptype, density, walkscore);

            //Get index table rows, including the calculated overall score
            client = _factory.CreateService();
            List<PropertyService.PropertiesIndex> indecies;
            if (filter != null)
            {
                indecies = client.SafeExecution(c => c.GetIndecies(filter, null)).ToList();
            }
            else
            {
                indecies = client.SafeExecution(c => c.GetAllIndecies()).ToList();
            }
            
            
            var model = new IndexModel
            {
                Records = Convert(indecies),
                Filter = Convert(filter),
                Sort = null,
                DropDowns = Convert(lookupValues)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IndexModel model, string proptype, string density, string walkscore)
        {
            // TODO: Not getting querystring values on post.  Figure this out!!

            var client = _factory.CreateService();
            var lookupValues = client.SafeExecution(c => c.GetLookupValues());
            
            //Get filtered index records
            var queryStringFilter = GetFilterFromQueryString(proptype, density, walkscore);
            var filter = Convert(model.Filter);
            filter = MergeFilters(filter, queryStringFilter);
            var sort = Convert(model.Sort);
            
            client = _factory.CreateService();
            var indicies = client.SafeExecution(c => c.GetIndecies(filter, sort)).ToList();

            model.Filter = Convert(filter);
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

        private PropertyService.IndexFilter GetFilterFromQueryString(string proptype, string density, string walkscore)
        {
            PropertyService.IndexFilter filter = null;

            if (!String.IsNullOrEmpty(proptype))
            {
                if (proptype == "flats")
                    filter = new PropertyService.IndexFilter { PropertyTypes = new List<int> { 10 }.ToArray() };
                if (proptype == "twins")
                    filter = new PropertyService.IndexFilter { PropertyTypes = new List<int> { 7 }.ToArray() };
                if (proptype == "townhomes")
                    filter = new PropertyService.IndexFilter { PropertyTypes = new List<int> { 3, 4 }.ToArray() };
                if (proptype == "units")
                    filter = new PropertyService.IndexFilter { PropertyTypes = new List<int> { 2, 5, 11, 12, 13 }.ToArray() };
            }

            if (!String.IsNullOrEmpty(density))
            {
                Range<double?> range = null;
                if (density == "lessthan10")
                    range = new Range<double?> { HighValue = 9.999 };
                if (density == "10to13")
                    range = new Range<double?> { LowValue = 10, HighValue = 13.999 };
                if (density == "14to16")
                    range = new Range<double?> { LowValue = 14, HighValue = 16.999 };
                if (density == "17to20")
                    range = new Range<double?> { LowValue = 17, HighValue = 19.999 };
                if (density == "morethan20")
                    range = new Range<double?> { LowValue = 20 };
                if (range != null)
                    filter = new PropertyService.IndexFilter { DensityRange = Convert(range) };
            }

            if (!String.IsNullOrEmpty(walkscore))
            {
                Range<int?> range = null;
                if (walkscore == "High")
                    range = new Range<int?> { LowValue = 66 };
                if (walkscore == "Medium")
                    range = new Range<int?> { LowValue = 33, HighValue = 65 };
                if (walkscore == "Low")
                    range = new Range<int?> { HighValue = 32 };
                if (range != null)
                    filter = new PropertyService.IndexFilter { WalkscoreRange = Convert(range) };
            }

            return filter;
        }

        private PropertyService.IndexFilter MergeFilters(PropertyService.IndexFilter filter, PropertyService.IndexFilter queryStringFilter)
        {
            if (filter != null && queryStringFilter != null)
            {
                // Query string always takes precidence
                filter.PropertyTypes = queryStringFilter.PropertyTypes;
                filter.DensityRange = queryStringFilter.DensityRange;
                filter.WalkscoreRange = queryStringFilter.WalkscoreRange;
            }

            return filter ?? queryStringFilter;
        }

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

        private IndexFilter Convert(PropertyService.IndexFilter f)
        {
            if (f != null)
            {
                return new IndexFilter
                {
                    PropertyId = f.PropertyId,
                    City = f.City,
                    PropertyTypes = f.PropertyTypes != null ? f.PropertyTypes.ToList() : null,
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

        private Range<int?> Convert(PropertyService.RangeOfNullableOfint5F2dSckg range)
        {
            if (range != null)
            {
                return new Range<int?>
                {
                    LowValue = range.LowValue,
                    HighValue = range.HighValue
                };
            }
            return null;
        }

        private Range<double?> Convert(PropertyService.RangeOfNullableOfdouble5F2dSckg range)
        {
            if (range != null)
            {
                return new Range<double?>
                {
                    LowValue = range.LowValue,
                    HighValue = range.HighValue
                };
            }
            return null;
        }

        private PropertyService.IndexFilter Convert(IndexFilter f)
        {
            if (f != null)
            {
                return new PropertyService.IndexFilter
                {
                    PropertyId = f.PropertyId,
                    City = f.City,
                    PropertyTypes = f.PropertyType.HasValue ? new List<int> { f.PropertyType.Value }.ToArray() : new int[0],
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
