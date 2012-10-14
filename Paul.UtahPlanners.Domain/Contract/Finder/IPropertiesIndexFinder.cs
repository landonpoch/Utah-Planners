using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.Contract.Finder
{
    public interface IPropertiesIndexFinder
    {
        List<PropertiesIndex> FindIndecies(IndexFilter filter, IndexSort sort);
    }
}
