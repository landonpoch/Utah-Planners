using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.Contract.Repository
{
    public interface IPropertyRepository
    {
        void Add(Property property);
        List<Property> GetAllProperties();
        Property Get(int id);
        void Remove(Property property);
        KeyValuePair<int, int> GetShowcaseProperty();
    }
}
