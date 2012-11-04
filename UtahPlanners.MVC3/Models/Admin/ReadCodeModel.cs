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
        public ReadCodeModel()
        {}

        public SelectList CodeEnums
        {
            get
            {
                return new SelectList(Constants.LookupCodesDictionary, "Key", "Value");
            }
        }
        public int SelectedCode { get; set; }
        public Dictionary<int, CodeDetails> CodeData { get; set; }
        public int? SelectedId { get; set; }
    }

    public class CodeDetails
    {
        public string Description { get; set; }
        public int Weight { get; set; }
    }
}