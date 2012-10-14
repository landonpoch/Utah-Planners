using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain.DTO
{
    public class AdminPropertyIndexDTO
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public double Density { get; set; }
        public int Units { get; set; }
        public int YearBuilt { get; set; }
        public string AdminNotes { get; set; }
        public string NotFinished { get; set; }
    }
}
