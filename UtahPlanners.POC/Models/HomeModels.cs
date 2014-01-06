using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.POC.Models
{
    public class IndexModel
    {
        public List<LookupValueModel> PropertyTypes { get; set; }
    }

    public class LookupValueModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}