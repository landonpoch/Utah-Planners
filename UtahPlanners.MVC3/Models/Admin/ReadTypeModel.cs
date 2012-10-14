using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtahPlanners.MVC3.Presentation;

namespace UtahPlanners.MVC3.Models.Admin
{
    public class ReadTypeModel
    {
        public SelectList TypeEnums
        {
            get
            {
                return new SelectList(Constants.LookupTypesDictionary, "Key", "Value");
            }
        }
        public int SelectedType { get; set; }
        public Dictionary<int, string> KeyValuePairs { get; set; }
    }
}