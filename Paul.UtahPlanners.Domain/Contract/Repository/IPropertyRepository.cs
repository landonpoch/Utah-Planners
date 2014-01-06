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
        Property Get(Guid id);
        void Remove(Property property);
    }
}
