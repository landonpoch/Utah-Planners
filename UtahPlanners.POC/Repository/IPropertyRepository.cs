using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.POC.Domain;

namespace UtahPlanners.POC.Repository
{
    public interface IPropertyRepository
    {
        /// <summary>
        /// Adds a property to the repository.
        /// </summary>
        /// <param name="property">The property to add</param>
        /// <returns>The identifier of the newly added property</returns>
        object Add(Property property);

        List<Property> GetAllProperties();
    }
}