﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Domain.Contract.Service;

namespace UtahPlanners.Domain.Services
{
    public class ConfigSettings : IConfigSettings
    {
        private IUnitOfWorkFactory _factory;
        private Weight _weights;
        private LookupValues _lookupValues;

        public ConfigSettings(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

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

        public LookupValues LookupValues
        {
            get 
            {
                if (_lookupValues == null)
                {
                    LoadLookupValues();
                }
                return _lookupValues;
            }
        }

        public void ReloadLookupValues()
        {
            LoadLookupValues();
        }

        #endregion

        private void LoadWeights()
        {
            using (IUnitOfWork unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreateConfigRepository();
                _weights = repo.GetWeights();
            }
        }

        private void LoadLookupValues()
        {
            using (IUnitOfWork unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreateConfigRepository();
                _lookupValues = repo.GetLookupValues();
            }
        }

    }
}
