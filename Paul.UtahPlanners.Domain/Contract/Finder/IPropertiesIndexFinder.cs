using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Domain.DTO;

namespace UtahPlanners.Domain.Contract.Finder
{
    public interface IPropertiesIndexFinder
    {
        List<PropertyIndexDTO> FindIndecies(IndexFilter filter, IndexSort sort);
        List<AdminPropertyIndexDTO> FindAdminIndecies(PropertySort sort);
    }
}
