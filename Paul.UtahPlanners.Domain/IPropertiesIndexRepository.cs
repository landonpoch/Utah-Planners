using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain
{
    public interface IPropertiesIndexRepository
    {
        List<PropertiesIndex> GetIndecies(IndexFilter filter, IndexSort sort);
    }
}
