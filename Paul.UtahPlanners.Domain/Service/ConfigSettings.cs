using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Contract.Repository;

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
            set
            {
                ConfigRepo((unit, repo) =>
                {
                    var w = repo.GetWeights();
                    w.buildingEnclosure = value.buildingEnclosure;
                    w.commonAreas = value.commonAreas;
                    w.neighCondition = value.neighCondition;
                    w.streetConn = value.streetConn;
                    w.streetSaftey = value.streetSaftey;
                    w.streetWalk = value.streetWalk;
                    w.twoFiftyApts = value.twoFiftyApts;
                    w.twoFiftySingleFam = value.twoFiftySingleFam;
                    w.walkscore = value.walkscore;
                    unit.Commit();
                });
                LoadWeights();
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
            ConfigRepo((unit, repo) =>
            {
                _weights = repo.GetWeights();
            });
        }

        private void LoadLookupValues()
        {
            ConfigRepo((unit, repo) =>
            {
                _lookupValues = repo.GetLookupValues();
            });
        }

        private void ConfigRepo(Action<IUnitOfWork, IConfigRepository> action)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreateConfigRepository();
                action.Invoke(unit, repo);
            }
        }
    }
}
