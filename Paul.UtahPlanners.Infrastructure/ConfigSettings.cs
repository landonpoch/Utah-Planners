using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain;

namespace UtahPlanners.Infrastructure
{
    public class ConfigSettings : IConfigSettings
    {
        public Weight _weights;

        #region IConfigSettings Members

        public Weight Weights
        {
            get 
            {
                if (_weights == null)
                {
                    LoadWeights();
                }
                return _weights;
            }
        }

        public void ReloadWeights()
        {
            LoadWeights();
        }

        #endregion

        private void LoadWeights()
        {
            using (var context = new PropertiesDB())
            {
                _weights = context.Weights.FirstOrDefault();
            }
        }
    }
}
