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
            //Get a random image from the database
            return View();
        }

        public ActionResult Index()
        {
            //Get index table rows, including the calculated overall score
            var client = _factory.CreateService();
            var indecies = client.SafeExecution(c => c.GetIndex()).ToList();
            return View(Convert(indecies));
        }

        public ActionResult Property(int id)
        {
            //Get the details for a property
            var client = _factory.CreateService();
            var property = client.SafeExecution(c => c.GetProperty(id));
            return View(Convert(property));
        }

        public ActionResult About()
        {
            return View();
        }

        private Property Convert(PropertyService.Property property)
        {
            return new Property
            {
                
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

        /*
        private IQueryable<PropertiesIndex> SortAndFilter(IQueryable<PropertiesIndex> props)
        {
            bool isFirstSort = true;
            foreach (string key in Request.QueryString.Keys)
            {
                // Sorts when a sort key is found, otherwise performs the filter
                if (key.Contains("sort"))
                {
                    IndexColumn column = (IndexColumn)Enum.Parse(typeof(IndexColumn), key.Replace("sort", String.Empty), true);
                    string direction = Request.QueryString[key];
                    props = this.Sort(column, props, direction == "desc", isFirstSort);
                }
                else
                {
                    // TODO: Add filtering support
                }
                isFirstSort = false;
            }
            return props;
        }

        private IQueryable<PropertiesIndex> Sort(IndexColumn column, IQueryable<PropertiesIndex> props, bool isDescending, bool isFirstSort)
        {
            switch (column)
            {
                case IndexColumn.City:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.city, isDescending, isFirstSort);
                    break;
                case IndexColumn.Density:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.density, isDescending, isFirstSort);
                    break;
                case IndexColumn.Id:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.id, isDescending, isFirstSort);
                    break;
                case IndexColumn.Score:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.OverallScore, isDescending, isFirstSort);
                    break;
                case IndexColumn.SocioEcon:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.SocioEconDescription, isDescending, isFirstSort);
                    break;
                case IndexColumn.StreetType:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.StreetTypeDescription, isDescending, isFirstSort);
                    break;
                case IndexColumn.TwoFiftySF:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.twoFiftySingleFam, isDescending, isFirstSort);
                    break;
                case IndexColumn.Type:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.PropertyTypeDescription, isDescending, isFirstSort);
                    break;
                case IndexColumn.Units:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.units, isDescending, isFirstSort);
                    break;
                case IndexColumn.Walkability:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.StreetWalkDescription, isDescending, isFirstSort);
                    break;
                case IndexColumn.Walkscore:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.walkscore, isDescending, isFirstSort);
                    break;
                case IndexColumn.Year:
                    props = this.MultiColumnOrderedByWithDirection(props, p => p.yearBuilt, isDescending, isFirstSort);
                    break;
            }
            return props;
        }

        private IOrderedQueryable<PropertiesIndex> MultiColumnOrderedByWithDirection<PropertiesIndex, TKey>
            (IQueryable<PropertiesIndex> props, Expression<Func<PropertiesIndex, TKey>> keySelector, bool descending, bool isFirstSort)
        {
            return isFirstSort ? OrderByWithDirection(props, keySelector, descending) :
                ThenByWithDirection((IOrderedQueryable<PropertiesIndex>)props, keySelector, descending);
        }

        private IOrderedQueryable<PropertiesIndex> ThenByWithDirection<PropertiesIndex, TKey>
            (IOrderedQueryable<PropertiesIndex> props, Expression<Func<PropertiesIndex, TKey>> keySelector, bool descending)
        {
            return descending ? props.ThenByDescending(keySelector) :
                props.ThenBy(keySelector);
        }

        private IOrderedQueryable<PropertiesIndex> OrderByWithDirection<PropertiesIndex, TKey>
            (IQueryable<PropertiesIndex> props, Expression<Func<PropertiesIndex, TKey>> keySelector, bool descending)
        {
            return descending ? props.OrderByDescending(keySelector) :
                props.OrderBy(keySelector);
        }
         */
    }
}
