using System;
using System.Web.Mvc;
using UtahPlanners.MVC3.Models.Home;
using System.Collections.Generic;

namespace UtahPlanners.MVC3.Extensions
{
    public static class HelperExtensions
    {

        #region UrlHelperExtensions

        /// <summary>
        /// Used to support sorting.  Pass in the column that you would like to sort.  If 
        /// the column is already sorted, it will simply re-sort it descending or 
        /// ascending, whatever makes sense.
        /// </summary>
        /// <param name="helper">Used for extension method, passed in automatically</param>
        /// <param name="column">The column that requires sorting</param>
        /// <returns>A string representing the new QueryString</returns>
        public static string Sort(this UrlHelper helper, PropertyService.IndexColumn column)
        {
            var queryString = helper.RequestContext.HttpContext.Request.QueryString;
            string keyName = "sort" + column.ToString();

            // Creates the first QueryString parameter in the url
            string firstParam = String.Empty;
            if (!String.IsNullOrEmpty(queryString[keyName]) && queryString.Keys[0] == keyName)
            {
                if (queryString[keyName] == "asc")
                    firstParam = "?" + keyName + "=desc";
                else
                    firstParam = "?" + keyName + "=asc";
            }
            else
            {
                firstParam = "?" + keyName + "=asc";
            }

            // Maintains all other parameters
            string otherParams = String.Empty;
            foreach (var key in queryString.Keys)
            {
                // Doesn't maintain the old parameter if a new value is being specified for it here
                if (key.ToString() != keyName)
                    otherParams = otherParams + "&" + key + "=" + queryString[key.ToString()];
            }

            // Appends the new parameter to the querystring
            return firstParam + otherParams;
        }

        // TODO: Add support for filtering
        public static string Filter(this UrlHelper helper, PropertyService.IndexColumn column, string value)
        {
            return String.Empty;
        }

        #endregion

        #region HtmlHelperExtensions

        public static string IsActive(this HtmlHelper htmlHelper, string action, string controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                return "active";
            }
            return null;
        }

        #endregion

        public static string ToQueryString(this Address address)
        {
            return String.Join("+", address.Street, address.City, address.State, address.ZipCode);
        }
    }
}
