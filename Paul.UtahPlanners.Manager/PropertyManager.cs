using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Repository;

namespace UtahPlanners.Manager
{
    public class PropertyManager
    {

        PropertyRepository _repository;

        #region Contsructors

        public PropertyManager()
            :this(new PropertyRepository())
        {
        }

        public PropertyManager(PropertyRepository repository)
        {
            _repository = repository;
        }

        #endregion

        //public IQueryable<Property> GetProperties()
        //{
        //    throw new NotImplementedException();
        //}


    }
}
