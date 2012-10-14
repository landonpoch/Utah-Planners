using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.DTO
{
    public abstract class PropertyDTO
    {
        public int Id { get; set; }
        public AddressDTO Address { get; set; }
        public int Walkscore { get; set; }
        public int TwoFiftySingleFamily { get; set; }
        public int TwoFiftyAppartments { get; set; }
        public double Density { get; set; }
        public int Area { get; set; }
        public int Units { get; set; }
        public int YearBuilt { get; set; }
        public string Notes { get; set; }
        public List<PictureMetaData> PictureMetaData { get; set; }
    }
}
