using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.MVC3.Presentation;

namespace UtahPlanners.MVC3.Models.Admin
{
    public class ReadCodeModel
    {
        public SelectList CodeEnums
        {
            get
            {
                return new SelectList(Constants.LookupCodesDictionary, "Key", "Value");
            }
        }
        public int SelectedCode { get; set; }
        public Dictionary<int, Tuple<string, int>> CodeData { get; set; }
        public int? SelectedId { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
    }
}