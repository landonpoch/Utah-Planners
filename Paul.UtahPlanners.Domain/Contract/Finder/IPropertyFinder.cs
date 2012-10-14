using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Domain.DTO;

namespace UtahPlanners.Domain.Contract.Finder
{
    public interface IPropertyFinder
    {
        UserPropertyDTO FindProperty(int id);
        AdminPropertyDTO FindAdminProperty(int id);
    }
}
