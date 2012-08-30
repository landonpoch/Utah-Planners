using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.Infrastructure;
using System.Configuration;
using System.Web.Configuration;
using UtahPlanners.MVC3.Models.Home;
using System.Linq.Expressions;

namespace UtahPlanners.MVC3.Controllers
{
    public class HomeController : Controller
    {
        PropertyRepository _repo = new PropertyRepository();

        public ActionResult Default()
        {
            //Get a random image from the database
            return View();
        }

        public ActionResult Index()
        {
            //Get index table rows, including the calculated overall score
            IQueryable<PropertiesIndex> props = _repo.GetAllPropertiesIndexes();
            
            //Performs the sort and filter operations
            props = SortAndFilter(props);

            //Execute query and return the correct list of results according to the sort/filter
            return View(props.ToList());
        }

        public ActionResult Property(int id)
        {
            //Get the details for a property
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

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
    }
}
