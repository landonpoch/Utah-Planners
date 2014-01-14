using System;
using System.Linq;
using System.Collections.Generic;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Contract.Persistence;
using UtahPlanners.Domain.Entity;
using Paul.UtahPlanners.Application.Contract;
using UtahPlanners.Domain.DTO;

namespace Paul.UtahPlanners.Application
{
    public class PropertyService : IPropertyService
    {
        private IPersistenceFactory _factory;
        private IConfigSettings _settings;
        private ILogger _logger;

        public PropertyService(IPersistenceFactory factory, IConfigSettings settings, ILogger logger)
        {
            _factory = factory;
            _settings = settings;
            _logger = logger;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region IPropertyService Members

        #region Queries
        
        public List<PropertyIndexDTO> FindAllIndecies()
        {
            return FindIndecies(null, null);
        }

        public List<PropertyIndexDTO> FindIndecies(IndexFilter filter, IndexSort sort)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var finder = unit.CreateIndexFinder();
                return finder.FindIndecies(filter, sort);
            }
        }

        public List<AdminPropertyIndexDTO> FindAllAdminIndecies(PropertySort sort)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var finder = unit.CreateIndexFinder();
                var props = finder.FindAdminIndecies(sort);
                return props;
            }
        }
        
        public UserPropertyDTO FindUserProperty(int id)
        {
            //using (var unit = _factory.CreateUnitOfWork())
            //{
            //    var finder = unit.CreatePropertyFinder();
            //    var prop = finder.FindProperty(id, _settings.Weights);
            //    return prop;
            //}
            throw new NotImplementedException();
        }

        public AdminPropertyDTO FindAdminProperty(int id)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var finder = unit.CreatePropertyFinder();
                var prop = finder.FindAdminProperty(id);
                return prop;
            }
        }

        public List<CsvPropertyDTO> FindAllCsvProperties()
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var finder = unit.CreatePropertyFinder();
                var props = finder.FindAllCsvProperties();
                return props;
            }
        }

        public KeyValuePair<int, int> FindShowcaseProperty()
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var finder = unit.CreatePropertyFinder();
                var showcaseProp = finder.FindShowcaseProperty();
                return showcaseProp;
            }
        }

        public Picture FindPicture(int id)
        {
            //using (var unit = _factory.CreateUnitOfWork())
            //{
            //    var finder = unit.CreatePictureFinder();
            //    return finder.FindPicture(id);
            //}
            //throw new NotImplementedException();
            return null;
        }

        public LookupValues GetAllLookupValues()
        {
            return _settings.LookupValues;
        }

        public Dictionary<Guid, string> GetLookupTypes(LookupType lookupType)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var result = new Dictionary<Guid, string>();
                switch (lookupType)
                {
                    case LookupType.PropertyType:
                        result = GetAllLookupValues<PropertyType>(unit).ToDictionary(l => l.Id, l => l.Description);
                        break;
                    case LookupType.StreetType:
                        result = GetAllLookupValues<StreetType>(unit).ToDictionary(l => l.Id, l => l.Description);
                        break;
                    case LookupType.SocioEconType:
                        result = GetAllLookupValues<SocioEcon>(unit).ToDictionary(l => l.Id, l => l.Description);
                        break;
                }
                return result;
            }
        }

        public Dictionary<Guid, Tuple<string, int>> GetLookupCodes(LookupCode lookupCode)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var result = new Dictionary<Guid, Tuple<string, int>>();
                switch (lookupCode)
                {
                    case LookupCode.CommonCode:
                        result = GetAllLookupValues<CommonAreas>(unit)
                            .ToDictionary<CommonAreas, Guid, Tuple<string, int>>
                                (ca => ca.Id, ca => new Tuple<string, int>(ca.Description, ca.Weight));
                        break;
                    case LookupCode.EnclosureCode:
                        result = GetAllLookupValues<BuildingEnclosure>(unit)
                            .ToDictionary<BuildingEnclosure, Guid, Tuple<string, int>>
                                (cc => cc.Id, cc => new Tuple<string, int>(cc.Description, cc.Weight));
                        break;
                    case LookupCode.NeighborhoodCode:
                        result = GetAllLookupValues<NeighborhoodCondition>(unit)
                            .ToDictionary<NeighborhoodCondition, Guid, Tuple<string, int>>
                                (nc => nc.Id, nc => new Tuple<string, int>(nc.Description, nc.Weight));
                        break;
                    case LookupCode.StreetConnCode:
                        result = GetAllLookupValues<StreetConnectivity>(unit)
                            .ToDictionary<StreetConnectivity, Guid, Tuple<string, int>>
                                (sc => sc.Id, sc => new Tuple<string, int>(sc.Description, sc.Weight));
                        break;
                    case LookupCode.StreetSafetyCode:
                        result = GetAllLookupValues<StreetSafety>(unit)
                            .ToDictionary<StreetSafety, Guid, Tuple<string, int>>
                                (ss => ss.Id, ss => new Tuple<string, int>(ss.Description, ss.Weight));
                        break;
                    case LookupCode.StreetWalkCode:
                        result = GetAllLookupValues<StreetWalkability>(unit)
                            .ToDictionary<StreetWalkability, Guid, Tuple<string, int>>
                                (sw => sw.Id, sw => new Tuple<string, int>(sw.Description, sw.Weight));
                        break;
                }
                return result;
            }
        }

        public Weights GetWeights()
        {
            return _settings.Weights;
        }

        #endregion

        // TODO: Refactor to use commands
        #region Commands

        public int SaveProperty(AdminPropertyDTO dto)
        {
            //string errorMessage = "An error occured while saving the property";
            //return CommandWrapper(errorMessage, (IUnitOfWork unit) =>
            //{
            //    var propRepo = unit.CreatePropertyRepository();
            //    var picRepo = unit.CreatePictureRepository();
            //    if (dto.Id > 0)
            //    {
            //        var prop = propRepo.Get(dto.Id);
            //        ModifyProperty(ref prop, dto);
            //        foreach (var metaPic in dto.PictureMetaData)
            //        {
            //            var pic = picRepo.Get(metaPic.PictureId); // Very inefficient, look into table splitting
            //            ModifyPicture(ref pic, metaPic);
            //            if (metaPic.Delete)
            //            {
            //                picRepo.Remove(pic);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var prop = new Property();
            //        ModifyProperty(ref prop, dto);
            //        propRepo.Add(prop);
            //    }
            //    return dto.Id;
            //});
            throw new NotImplementedException();
        }

        public bool DeleteProperty(int propertyId)
        {
            //string message = "An error occurred while deleting the property";
            //return CommandWrapper(message, (IUnitOfWork unit) =>
            //{
            //    var repo = unit.CreatePropertyRepository();
            //    var property = repo.Get(propertyId);
            //    repo.Remove(property);
            //});
            throw new NotImplementedException();
        }

        public bool CreateLookupType(LookupType lookupType, string value)
        {
            string message = "An error occured while trying to create a new lookup type";
            return CommandWrapper(message, (IUnitOfWork unit) =>
            {
                switch (lookupType)
                {
                    case LookupType.PropertyType:
                        CreateLookupValue(unit, new PropertyType(value));
                        break;
                    case LookupType.StreetType:
                        CreateLookupValue(unit, new StreetType(value));
                        break;
                    case LookupType.SocioEconType:
                        CreateLookupValue(unit, new SocioEcon(value));
                        break;
                }
            });
        }

        public bool ModifyLookupType(LookupType lookupType, Guid id, string value)
        {
            bool result = false;
            switch (lookupType)
            {
                case LookupType.PropertyType:
                    result = UpdateLookupValue(id, new PropertyType(value));
                    break;
                case LookupType.StreetType:
                    result = UpdateLookupValue(id, new StreetType(value));
                    break;
                case LookupType.SocioEconType:
                    result = UpdateLookupValue(id, new SocioEcon(value));
                    break;
            }
            return result;
        }

        public bool DeleteLookupType(LookupType lookupType, Guid id)
        {
            string message = "An error occured while trying to delete the lookup type.";
            return CommandWrapper(message, (IUnitOfWork unit) =>
            {
                switch (lookupType)
                {
                    case LookupType.PropertyType:
                        RemoveLookupValue<PropertyType>(unit, id);
                        break;
                    case LookupType.StreetType:
                        RemoveLookupValue<StreetType>(unit, id);
                        break;
                    case LookupType.SocioEconType:
                        RemoveLookupValue<SocioEcon>(unit, id);
                        break;
                }
            });
        }

        public bool CreateLookupCode(LookupCode lookupCode, string value, int weight)
        {
            string errorMessage = "An error occured while tring to create the lookup code.";
            return CommandWrapper(errorMessage, (IUnitOfWork unit) =>
            {
                switch (lookupCode)
                {
                    case LookupCode.CommonCode:
                        CreateLookupValue(unit, new CommonAreas(value, weight));
                        break;
                    case LookupCode.EnclosureCode:
                        CreateLookupValue(unit, new BuildingEnclosure(value, weight));
                        break;
                    case LookupCode.NeighborhoodCode:
                        CreateLookupValue(unit, new NeighborhoodCondition(value, weight));
                        break;
                    case LookupCode.StreetConnCode:
                        CreateLookupValue(unit, new StreetConnectivity(value, weight));
                        break;
                    case LookupCode.StreetSafetyCode:
                        CreateLookupValue(unit, new StreetSafety(value, weight));
                        break;
                    case LookupCode.StreetWalkCode:
                        CreateLookupValue(unit, new StreetWalkability(value, weight));
                        break;
                }
            });
        }

        public bool ModifyLookupCode(LookupCode lookupCode, Guid id, string value, int weight)
        {
            //string errorMessage = "An error occured while tring to modify the lookup code.";
            //return CommandWrapper(errorMessage, (IUnitOfWork unit) =>
            //{
            //    switch (lookupCode)
            //    {
            //        case LookupCode.CommonCode:
            //            var commonCode = GetLookupValue<CommonCode>(unit, id);
            //            commonCode.description = value;
            //            commonCode.weight = weight;
            //            break;
            //        case LookupCode.EnclosureCode:
            //            var enclosureCode = GetLookupValue<EnclosureCode>(unit, id);
            //            enclosureCode.description = value;
            //            enclosureCode.weight = weight;
            //            break;
            //        case LookupCode.NeighborhoodCode:
            //            var neighborhoodCode = GetLookupValue<NeighborhoodCode>(unit, id);
            //            neighborhoodCode.description = value;
            //            neighborhoodCode.weight = weight;
            //            break;
            //        case LookupCode.StreetConnCode:
            //            var streetConnCode = GetLookupValue<StreetconnCode>(unit, id);
            //            streetConnCode.description = value;
            //            streetConnCode.weight = weight;
            //            break;
            //        case LookupCode.StreetSafetyCode:
            //            var streetSafetyCode = GetLookupValue<StreetSafteyCode>(unit, id);
            //            streetSafetyCode.description = value;
            //            streetSafetyCode.weight = weight;
            //            break;
            //        case LookupCode.StreetWalkCode:
            //            var streetWalkCode = GetLookupValue<StreetwalkCode>(unit, id);
            //            streetWalkCode.description = value;
            //            streetWalkCode.weight = weight;
            //            break;
            //    }
            //});
            throw new NotImplementedException();
        }

        public bool DeleteLookupCode(LookupCode lookupCode, Guid id)
        {
            string errorMessage = "An error occured while tring to delete the lookup code.";
            return CommandWrapper(errorMessage, (IUnitOfWork unit) =>
            {
                switch (lookupCode)
                {
                    case LookupCode.CommonCode:
                        RemoveLookupValue<CommonAreas>(unit, id);
                        break;
                    case LookupCode.EnclosureCode:
                        RemoveLookupValue<BuildingEnclosure>(unit, id);
                        break;
                    case LookupCode.NeighborhoodCode:
                        RemoveLookupValue<NeighborhoodCondition>(unit, id);
                        break;
                    case LookupCode.StreetConnCode:
                        RemoveLookupValue<StreetConnectivity>(unit, id);
                        break;
                    case LookupCode.StreetSafetyCode:
                        RemoveLookupValue<StreetSafety>(unit, id);
                        break;
                    case LookupCode.StreetWalkCode:
                        RemoveLookupValue<StreetWalkability>(unit, id);
                        break;
                }
            });
        }

        public bool UpdateWeights(Guid id, Weights weights)
        {
            bool result = false;
            try
            {
                var repo = _factory.CreatePersistentRepository<Weights>();
                if (id == default(Guid))
                {
                    repo.Insert(weights);
                    result = true;
                }
                else
                {
                    result = repo.Update(id, weights);
                }
            }
            catch (Exception e)
            {
                _logger.Error("An error occured while trying to update weights", e);
            }
            return result;
        }

        public bool UploadPicture(Picture pic)
        {
            //string errorMessage = "An error occured while trying to upload a picture";
            //return CommandWrapper(errorMessage, (unit) =>
            //{
            //    var repo = unit.CreatePictureRepository();
            //    repo.Add(pic);
            //});
            throw new NotImplementedException();
        }

        #endregion
        
        #endregion

        #region Private Methods

        private int CommandWrapper(string errorMessage, Func<IUnitOfWork, int> func)
        {
            var result = 0;
            try
            {
                using (var unit = _factory.CreateUnitOfWork())
                {
                    result = func.Invoke(unit);
                    unit.Commit();
                }
            }
            catch (Exception e)
            {
                _logger.Warning(errorMessage, e);
            }
            return result;
        }

        private bool CommandWrapper(string errorMessage, Action<IUnitOfWork> action)
        {
            bool result = false;
            try
            {
                using (var unit = _factory.CreateUnitOfWork())
                {
                    action.Invoke(unit);
                    unit.Commit();
                    result = true;
                }
            }
            catch (Exception e)
            {
                _logger.Warning(errorMessage, e);
            }
            return result;
        }

        private void CreateLookupValue<T>(IUnitOfWork unit, T lookupValue)
            where T : LookupValue
        {
            var repo = unit.CreateLookupValueRepository<T>();
            repo.AddLookupValue(lookupValue);
        }

        private T GetLookupValue<T>(IUnitOfWork unit, Guid id)
            where T : LookupValue
        {
            var repo = unit.CreateLookupValueRepository<T>();
            return repo.GetLookupValue(id);
        }

        private void RemoveLookupValue<T>(IUnitOfWork unit, Guid id)
            where T : LookupValue
        {
            var repo = unit.CreateLookupValueRepository<T>();
            repo.RemoveLookupValue(id);
        }

        private List<T> GetAllLookupValues<T>(IUnitOfWork unit)
            where T : LookupValue
        {
            var repo = unit.CreateLookupValueRepository<T>();
            return repo.GetAllLookupValues();
        }

        private bool UpdateLookupValue<T>(Guid id, T lookupValue)
            where T : LookupValue
        {
            var repo = _factory.CreatePersistentRepository<T>();
            return repo.Update(id, lookupValue);
        }

        #endregion

        #region Mappers

        private void ModifyProperty(ref Property prop, AdminPropertyDTO dto)
        {
            //prop.Address = Convert(dto.Address, dto.Id);
            //prop.adminNotes = dto.AdminNotes;
            //prop.area = dto.Area;
            //prop.buildingEnclosure = dto.BuildingEnclosure;
            //prop.commonAreas = dto.CommonAreas;
            //prop.density = dto.Density;
            //prop.neighCondition = dto.NeighborhoodCondition;
            //prop.notes = dto.Notes;
            //prop.notFinished = dto.NotFinished;
            //prop.socioEcon = dto.SocioEcon;
            //prop.streetCode = dto.StreetType;
            //prop.streetConn = dto.StreetConnectivity;
            //prop.streetSaftey = dto.StreetSafety;
            //prop.streetWalk = dto.StreetWalkability;
            //prop.twoFiftyApts = dto.TwoFiftyAppartments;
            //prop.twoFiftySingleFam = dto.TwoFiftySingleFamily;
            //prop.typeCode = dto.Type;
            //prop.units = dto.Units;
            //prop.walkscore = dto.Walkscore;
            //prop.walkscoreNotes = dto.WalkscoreNotes;
            //prop.yearBuilt = dto.YearBuilt;
            throw new NotImplementedException();
        }

        private Address Convert(AddressDTO dto, int id)
        {
            //return new Address
            //{
            //    City = dto.City,
            //    Country = dto.Country,
            //    Id = id,
            //    State = dto.State,
            //    Street1 = dto.Street1,
            //    Street2 = dto.Street2,
            //    Zip = dto.Zip
            //};
            throw new NotImplementedException();
        }

        private void ModifyPicture(ref Picture pic, PictureMetaData meta)
        {
            //pic.frontPage = (short)(meta.FrontPage ? 1 : 0);
            //pic.mainPicture = (short)(meta.PrimaryPicture ? 1 : 0);
            //pic.secondaryPicture = (short)(meta.SecondaryPicture ? 1 : 0);
            throw new NotImplementedException();
        }

        #endregion
    }
}
