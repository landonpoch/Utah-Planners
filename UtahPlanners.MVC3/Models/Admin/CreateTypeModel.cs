using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UtahPlanners.MVC3.Models.Admin
{
    public class CreateTypeModel
    {
        public static Dictionary<int, string> TypeDictionary = new Dictionary<int, string>
        {
            { 1, "Property Type" },
            { 2, "Street Type" },
            { 3, "Socio-Econ Type" }
        };
        public SelectList TypeEnums
        {
            get
            {
                return new SelectList(TypeDictionary, "Key", "Value");
            }
        }
        public int? SelectedType { get; set; }
        public string TypeDescription { get; set; }
    }
}