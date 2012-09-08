using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace UtahPlanners.Domain.Entity
{
    public class IndexSort
    {
        public IndexColumn Column { get; set; }
        public Direction Direction { get; set; }
    }

    public enum IndexColumn
    {
        City,
        Id,
        Score,
        Type,
        Density,
        Units,
        Year,
        StreetType,
        Walkability,
        Walkscore,
        SocioEcon,
        TwoFiftySF
    }

    public enum Direction
    {
        Ascending,
        Descending
    }
}
