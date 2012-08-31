using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain
{
    public interface IPropertyRepository
    {
        Property Get(int id);
        List<Property> GetAll();
    }
}
