using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain;
using System.Data.Entity;

namespace UtahPlanners.Infrastructure
{
    public class PropertiesIndexRepository : IPropertiesIndexRepository
    {
        private DbSet<PropertiesIndex> _props;

        public PropertiesIndexRepository(DbSet<PropertiesIndex> props)
        {
            _props = props;
        }

        #region IPropertiesIndexRepository Members

        public List<PropertiesIndex> GetAll()
        {
            return _props.ToList();
        }

        #endregion
    }
}
