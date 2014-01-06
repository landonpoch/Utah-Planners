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
        private Weights _weights;
        private LookupValues _lookupValues;

        public ConfigSettings(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        #region IConfigSettings Members

        public Weights Weights
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
                    w.BuildingEnclosure = value.BuildingEnclosure;
                    w.CommonAreas = value.CommonAreas;
                    w.NeighborhoodCondition = value.NeighborhoodCondition;
                    w.StreetConnectivity = value.StreetConnectivity;
                    w.StreetSafety = value.StreetSafety;
                    w.StreetWalkability = value.StreetWalkability;
                    w.TwoFiftyApartments = value.TwoFiftyApartments;
                    w.TwoFiftySingleFamily = value.TwoFiftySingleFamily;
                    w.Walkscore = value.Walkscore;
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
