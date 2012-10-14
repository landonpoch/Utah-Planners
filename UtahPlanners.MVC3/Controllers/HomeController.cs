using System.Web.Mvc;
using UtahPlanners.MVC3.Extensions;
using UtahPlanners.MVC3.Services;
using UtahPlanners.MVC3.Models.Home;
using System;
using System.Linq;
using System.Collections.Generic;
using UtahPlanners.MVC3.Helper;

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
            using (var wcf = _factory.CreatePropertyServiceWrapper())
            {
                var prop = wcf.Client.GetShowcaseProperty();
                return View(prop);
            }
        }

        public ActionResult Index(string proptype, string density, string walkscore)
        {
            using (var client = _factory.CreatePropertyServiceProxy())
            {
                var lookupValues = client.GetLookupValues();

                // Get default filter and sort
                PropertyService.IndexFilter filter = GetPrimaryFilterFromModel(proptype, density, walkscore);
                PropertyService.IndexSort sort = null;
                
                //Get index table rows, including the calculated overall score
                List<PropertyService.PropertiesIndex> indecies = client.GetIndecies(filter, sort).ToList();

                var model = new IndexModel
                {
                    IndexGridModel = new IndexGridModel
                    {
                        Records = Convert(indecies),
                        Filter = Convert(filter),
                        Sort = Convert(sort)
                    },
                    DropDowns = Convert(lookupValues),
                    PropType = proptype,
                    Density = density,
                    Walkscore = walkscore
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult IndexGrid(IndexModel model)
        {
            PropertyService.IndexFilter filter = GetPrimaryFilterFromModel(model.PropType, model.Density, model.Walkscore);
            filter = filter ?? Convert(model.IndexGridModel.Filter);
            var sort = Convert(model.IndexGridModel.Sort);

            using (var wcf = _factory.CreatePropertyServiceWrapper())
            {
                var indicies = wcf.Client.GetIndecies(filter, sort).ToList();
                model = new IndexModel
                {
                    IndexGridModel = new IndexGridModel
                    {
                        Records = Convert(indicies),
                        Filter = Convert(filter),
                        Sort = Convert(sort)
                    },
                    PropType = model.PropType,
                    Density = model.Density,
                    Walkscore = model.Walkscore
                };
                return PartialView("~/Views/Home/EditorTemplates/IndexGridModel.cshtml", model.IndexGridModel);
            }
        }

        public ActionResult Property(int id)
        {
            //Get the details for a property
            using (var wcf = _factory.CreatePropertyServiceWrapper())
            {
                var property = wcf.Client.GetUserProperty(id);
                var prop = Convert(property);
                return View(prop);
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult GetPicture(int id)
        {
            using (var wcf = _factory.CreatePropertyServiceWrapper())
            {
                var picture = wcf.Client.GetPicture(id);
                return File(picture.binaryData, "image/png");
            }
        }

        #region Mapping Methods

        private PropertyService.IndexFilter GetPrimaryFilterFromModel(string proptype, string density, string walkscore)
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

        private Property Convert(PropertyService.UserPropertyDTO p)
        {
            return new Property
            {
                Id = p.Id,
                Address = Convert(p.Address),
                PictureIds = p.PictureMetaData
                    .ToList()
                    .Where(md => !md.SecondaryPicture)
                    .Select(md => md.PictureId)
                    .ToList(),
                SecondaryPictureId = p.PictureMetaData
                    .ToList()
                    .Where(md => md.SecondaryPicture)
                    .Select(md => md.PictureId)
                    .FirstOrDefault(),
                PropertyType = p.Type,
                Score = p.Score,
                StreetSafety = p.StreetSafety,
                BuildingEnclosure = p.BuildingEnclosure,
                CommonAreas = p.CommonAreas,
                StreetConnectivity = p.StreetConnectivity,
                StreetWalkability = p.StreetWalkability,
                Walkscore = p.Walkscore,
                NeighborhoodCondition = p.NeighborhoodCondition,
                TwoFiftySingleFamily = p.TwoFiftySingleFamily,
                TwoFiftyApartments = p.TwoFiftyAppartments,
                Density = p.Density,
                Area = p.Area,
                Units = p.Units,
                StreetType = p.StreetType,
                YearBuilt = p.YearBuilt,
                SocioEcon = p.SocioEcon,
                Notes = p.Notes
            };
        }

        private Address Convert(PropertyService.AddressDTO a)
        {
            return new Address
            {
                Street = a.Street1,
                City = a.City,
                State = a.State,
                ZipCode = a.Zip
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

        private IndexColumn Convert(PropertyService.IndexColumn c)
        {
            switch (c)
            {
                case PropertyService.IndexColumn.City:
                    return IndexColumn.City;
                case PropertyService.IndexColumn.Score:
                    return IndexColumn.Score;
                case PropertyService.IndexColumn.Type:
                    return IndexColumn.Type;
                case PropertyService.IndexColumn.Density:
                    return IndexColumn.Density;
                case PropertyService.IndexColumn.Units:
                    return IndexColumn.Units;
                case PropertyService.IndexColumn.Year:
                    return IndexColumn.Year;
                case PropertyService.IndexColumn.StreetType:
                    return IndexColumn.StreetType;
                case PropertyService.IndexColumn.Walkability:
                    return IndexColumn.Walkability;
                case PropertyService.IndexColumn.Walkscore:
                    return IndexColumn.Walkscore;
                case PropertyService.IndexColumn.SocioEcon:
                    return IndexColumn.SocioEcon;
                case PropertyService.IndexColumn.TwoFiftySF:
                    return IndexColumn.TwoFiftySF;
                case PropertyService.IndexColumn.Id:
                default:
                    return IndexColumn.Id;
            }
        }

        private IndexSort Convert(PropertyService.IndexSort sort)
        {
            if (sort != null)
            {
                return new IndexSort
                {
                    Column = Convert(sort.Column),
                    Direction = Convert(sort.Direction),
                };
            }
            return null;
        }

        private Direction Convert(PropertyService.Direction d)
        {
            switch (d)
            {
                case PropertyService.Direction.Descending:
                    return Direction.Descending;
                case PropertyService.Direction.Ascending:
                default:
                    return Direction.Ascending;
            }
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
