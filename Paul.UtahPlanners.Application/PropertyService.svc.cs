using System;
using System.Linq;
using System.Collections.Generic;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Domain.Entity;
using Paul.UtahPlanners.Application.Contract;
using UtahPlanners.Domain.DTO;

namespace Paul.UtahPlanners.Application
{
    public class PropertyService : IPropertyService
    {
        private IUnitOfWorkFactory _factory;
        private IConfigSettings _settings;
        private ILogger _logger;

        public PropertyService(IUnitOfWorkFactory factory, IConfigSettings settings, ILogger logger)
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
        
        public List<PropertiesIndex> FindAllIndecies()
        {
            return FindIndecies(null, null);
        }

        public List<PropertiesIndex> FindIndecies(IndexFilter filter, IndexSort sort)
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
            using (var unit = _factory.CreateUnitOfWork())
            {
                var finder = unit.CreatePropertyFinder();
                var prop = finder.FindProperty(id, _settings.Weights);
                return prop;
            }
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
            using (var unit = _factory.CreateUnitOfWork())
            {
                var finder = unit.CreatePictureFinder();
                return finder.FindPicture(id);
            }
        }

        public LookupValues GetAllLookupValues()
        {
            return _settings.LookupValues;
        }

        public Dictionary<int, string> GetLookupTypes(LookupType lookupType)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var result = new Dictionary<int, string>();
                switch (lookupType)
                {
                    case LookupType.PropertyType:
                        result = GetAllLookupValues<PropertyType>(unit).ToDictionary(l => l.id, l => l.description);
                        break;
                    case LookupType.StreetType:
                        result = GetAllLookupValues<StreetType>(unit).ToDictionary(l => l.id, l => l.description);
                        break;
                    case LookupType.SocioEconType:
                        result = GetAllLookupValues<SocioEconCode>(unit).ToDictionary(l => l.id, l => l.description);
                        break;
                }
                return result;
            }
        }

        public Dictionary<int, Tuple<string, int>> GetLookupCodes(LookupCode lookupCode)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var result = new Dictionary<int, Tuple<string, int>>();
                switch (lookupCode)
                {
                    case LookupCode.CommonCode:
                        result = GetAllLookupValues<CommonCode>(unit)
                            .ToDictionary<CommonCode, int, Tuple<string, int>>
                                (cc => cc.id, cc => new Tuple<string, int>(cc.description, cc.weight.Value));
                        break;
                    case LookupCode.EnclosureCode:
                        result = GetAllLookupValues<EnclosureCode>(unit)
                            .ToDictionary<EnclosureCode, int, Tuple<string, int>>
                                (cc => cc.id, cc => new Tuple<string, int>(cc.description, cc.weight.Value));
                        break;
                    case LookupCode.NeighborhoodCode:
                        result = GetAllLookupValues<NeighborhoodCode>(unit)
                            .ToDictionary<NeighborhoodCode, int, Tuple<string, int>>
                                (cc => cc.id, cc => new Tuple<string, int>(cc.description, cc.weight.Value));
                        break;
                    case LookupCode.StreetConnCode:
                        result = GetAllLookupValues<StreetconnCode>(unit)
                            .ToDictionary<StreetconnCode, int, Tuple<string, int>>
                                (cc => cc.id, cc => new Tuple<string, int>(cc.description, cc.weight.Value));
                        break;
                    case LookupCode.StreetSafetyCode:
                        result = GetAllLookupValues<StreetSafteyCode>(unit)
                            .ToDictionary<StreetSafteyCode, int, Tuple<string, int>>
                                (cc => cc.id, cc => new Tuple<string, int>(cc.description, cc.weight.Value));
                        break;
                    case LookupCode.StreetWalkCode:
                        result = GetAllLookupValues<StreetwalkCode>(unit)
                            .ToDictionary<StreetwalkCode, int, Tuple<string, int>>
                                (cc => cc.id, cc => new Tuple<string, int>(cc.description, cc.weight.Value));
                        break;
                }
                return result;
            }
        }

        public Weight GetWeights()
        {
            return _settings.Weights;
        }

        #endregion

        // TODO: Refactor to use commands
        #region Commands

        public int SaveProperty(AdminPropertyDTO dto)
        {
            string errorMessage = "An error occured while saving the property";
            return CommandWrapper(errorMessage, (IUnitOfWork unit) =>
            {
                var propRepo = unit.CreatePropertyRepository();
                var picRepo = unit.CreatePictureRepository();
                if (dto.Id > 0)
                {
                    var prop = propRepo.Get(dto.Id);
                    ModifyProperty(ref prop, dto);
                    foreach (var metaPic in dto.PictureMetaData)
                    {
                        var pic = picRepo.Get(metaPic.PictureId); // Very inefficient, look into table splitting
                        ModifyPicture(ref pic, metaPic);
                        if (metaPic.Delete)
                        {
                            picRepo.Remove(pic);
                        }
                    }
                }
                else
                {
                    var prop = new Property();
                    ModifyProperty(ref prop, dto);
                    propRepo.Add(prop);
                }
                return dto.Id;
            });
        }

        public bool DeleteProperty(int propertyId)
        {
            string message = "An error occurred while deleting the property";
            return CommandWrapper(message, (IUnitOfWork unit) =>
            {
                var repo = unit.CreatePropertyRepository();
                var property = repo.Get(propertyId);
                repo.Remove(property);
            });
        }

        public bool CreateLookupType(LookupType lookupType, string value)
        {
            string message = "An error occured while trying to create a new lookup type";
            return CommandWrapper(message, (IUnitOfWork unit) =>
            {
                switch (lookupType)
                {
                    case LookupType.PropertyType:
                        CreateLookupValue(unit, new PropertyType { description = value });
                        break;
                    case LookupType.StreetType:
                        CreateLookupValue(unit, new StreetType { description = value });
                        break;
                    case LookupType.SocioEconType:
                        CreateLookupValue(unit, new SocioEconCode { description = value });
                        break;
                }
            });
        }

        public bool ModifyLookupType(LookupType lookupType, int id, string value)
        {
            string message = "An error occured while modifying the lookup type.";
            return CommandWrapper(message, (IUnitOfWork unit) =>
            { 
                switch (lookupType)
                {
                    case LookupType.PropertyType:
                        GetLookupValue<PropertyType>(unit, id).description = value;
                        break;
                    case LookupType.StreetType:
                        GetLookupValue<StreetType>(unit, id).description = value;
                        break;
                    case LookupType.SocioEconType:
                        GetLookupValue<SocioEconCode>(unit, id).description = value;
                        break;
                }
            });
        }

        public bool DeleteLookupType(LookupType lookupType, int id)
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
                        RemoveLookupValue<SocioEconCode>(unit, id);
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
                        CreateLookupValue(unit, new CommonCode { description = value, weight = weight });
                        break;
                    case LookupCode.EnclosureCode:
                        CreateLookupValue(unit, new EnclosureCode { description = value, weight = weight });
                        break;
                    case LookupCode.NeighborhoodCode:
                        CreateLookupValue(unit, new NeighborhoodCode { description = value, weight = weight });
                        break;
                    case LookupCode.StreetConnCode:
                        CreateLookupValue(unit, new StreetconnCode { description = value, weight = weight });
                        break;
                    case LookupCode.StreetSafetyCode:
                        CreateLookupValue(unit, new StreetSafteyCode { description = value, weight = weight });
                        break;
                    case LookupCode.StreetWalkCode:
                        CreateLookupValue(unit, new StreetwalkCode { description = value, weight = weight });
                        break;
                }
            });
        }

        public bool ModifyLookupCode(LookupCode lookupCode, int id, string value, int weight)
        {
            string errorMessage = "An error occured while tring to modify the lookup code.";
            return CommandWrapper(errorMessage, (IUnitOfWork unit) =>
            {
                switch (lookupCode)
                {
                    case LookupCode.CommonCode:
                        var commonCode = GetLookupValue<CommonCode>(unit, id);
                        commonCode.description = value;
                        commonCode.weight = weight;
                        break;
                    case LookupCode.EnclosureCode:
                        var enclosureCode = GetLookupValue<EnclosureCode>(unit, id);
                        enclosureCode.description = value;
                        enclosureCode.weight = weight;
                        break;
                    case LookupCode.NeighborhoodCode:
                        var neighborhoodCode = GetLookupValue<NeighborhoodCode>(unit, id);
                        neighborhoodCode.description = value;
                        neighborhoodCode.weight = weight;
                        break;
                    case LookupCode.StreetConnCode:
                        var streetConnCode = GetLookupValue<StreetconnCode>(unit, id);
                        streetConnCode.description = value;
                        streetConnCode.weight = weight;
                        break;
                    case LookupCode.StreetSafetyCode:
                        var streetSafetyCode = GetLookupValue<StreetSafteyCode>(unit, id);
                        streetSafetyCode.description = value;
                        streetSafetyCode.weight = weight;
                        break;
                    case LookupCode.StreetWalkCode:
                        var streetWalkCode = GetLookupValue<StreetwalkCode>(unit, id);
                        streetWalkCode.description = value;
                        streetWalkCode.weight = weight;
                        break;
                }
            });
        }

        public bool DeleteLookupCode(LookupCode lookupCode, int id)
        {
            string errorMessage = "An error occured while tring to delete the lookup code.";
            return CommandWrapper(errorMessage, (IUnitOfWork unit) =>
            {
                switch (lookupCode)
                {
                    case LookupCode.CommonCode:
                        RemoveLookupValue<CommonCode>(unit, id);
                        break;
                    case LookupCode.EnclosureCode:
                        RemoveLookupValue<EnclosureCode>(unit, id);
                        break;
                    case LookupCode.NeighborhoodCode:
                        RemoveLookupValue<NeighborhoodCode>(unit, id);
                        break;
                    case LookupCode.StreetConnCode:
                        RemoveLookupValue<StreetconnCode>(unit, id);
                        break;
                    case LookupCode.StreetSafetyCode:
                        RemoveLookupValue<StreetSafteyCode>(unit, id);
                        break;
                    case LookupCode.StreetWalkCode:
                        RemoveLookupValue<StreetwalkCode>(unit, id);
                        break;
                }
            });
        }

        public bool UpdateWeights(Weight weights)
        {
            bool result = false;
            try
            {
                _settings.Weights = weights;
            }
            catch (Exception e)
            {
                _logger.Error("An error occured while trying to update weights", e);
            }
            return result;
        }

        public bool UploadPicture(Picture pic)
        {
            string errorMessage = "An error occured while trying to upload a picture";
            return CommandWrapper(errorMessage, (unit) =>
            {
                var repo = unit.CreatePictureRepository();
                repo.Add(pic);
            });
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
            where T : class
        {
            var repo = unit.CreateLookupValueRepository<T>();
            repo.AddLookupValue(lookupValue);
        }

        private T GetLookupValue<T>(IUnitOfWork unit, int id)
            where T : class
        {
            var repo = unit.CreateLookupValueRepository<T>();
            return repo.GetLookupValue(id);
        }

        private void RemoveLookupValue<T>(IUnitOfWork unit, int id)
            where T : class
        {
            var repo = unit.CreateLookupValueRepository<T>();
            repo.RemoveLookupValue(id);
        }

        private List<T> GetAllLookupValues<T>(IUnitOfWork unit)
            where T : class
        {
            var repo = unit.CreateLookupValueRepository<T>();
            return repo.GetAllLookupValues();
        }

        #endregion

        #region Mappers

        private void ModifyProperty(ref Property prop, AdminPropertyDTO dto)
        {
            prop.Address = Convert(dto.Address, dto.Id);
            prop.adminNotes = dto.AdminNotes;
            prop.area = dto.Area;
            prop.buildingEnclosure = dto.BuildingEnclosure;
            prop.commonAreas = dto.CommonAreas;
            prop.density = dto.Density;
            prop.neighCondition = dto.NeighborhoodCondition;
            prop.notes = dto.Notes;
            prop.notFinished = dto.NotFinished;
            prop.socioEcon = dto.SocioEcon;
            prop.streetCode = dto.StreetType;
            prop.streetConn = dto.StreetConnectivity;
            prop.streetSaftey = dto.StreetSafety;
            prop.streetWalk = dto.StreetWalkability;
            prop.twoFiftyApts = dto.TwoFiftyAppartments;
            prop.twoFiftySingleFam = dto.TwoFiftySingleFamily;
            prop.typeCode = dto.Type;
            prop.units = dto.Units;
            prop.walkscore = dto.Walkscore;
            prop.walkscoreNotes = dto.WalkscoreNotes;
            prop.yearBuilt = dto.YearBuilt;
        }

        private Address Convert(AddressDTO dto, int id)
        {
            return new Address
            {
                city = dto.City,
                country = dto.Country,
                id = id,
                state = dto.State,
                street1 = dto.Street1,
                street2 = dto.Street2,
                zip = dto.Zip
            };
        }

        private void ModifyPicture(ref Picture pic, PictureMetaData meta)
        {
            pic.frontPage = (short)(meta.FrontPage ? 1 : 0);
            pic.mainPicture = (short)(meta.PrimaryPicture ? 1 : 0);
            pic.secondaryPicture = (short)(meta.SecondaryPicture ? 1 : 0);
        }

        #endregion
    }
}
