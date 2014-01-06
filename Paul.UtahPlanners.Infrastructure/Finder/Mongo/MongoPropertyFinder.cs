using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Contract.Finder;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.DTO;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Infrastructure.Shared;

namespace UtahPlanners.Infrastructure.Finder.Mongo
{
    public class MongoPropertyFinder : IPropertyFinder
    {
        private MongoDatabase _db;
        private IPropertyRepository _propRepo;
        private PropertyContext _context;

        public MongoPropertyFinder(MongoDatabase db, IPropertyRepository propRepo, PropertyContext context)
        {
            _db = db;
            _propRepo = propRepo;
            _context = context;
        }

        public UserPropertyDTO FindProperty(Guid id, Weights weights)
        {
            //var prop = _propRepo.Get(id);
            //return new UserPropertyDTO
            //{
            //    Id = prop.Id,
            //    Address = Convert(prop.Address),
            //    Area = prop.Area.GetValueOrDefault(),
            //    Density = prop.Density.GetValueOrDefault(),
            //    TwoFiftyAppartments = prop.TwoFiftyApartments.GetValueOrDefault(),
            //    TwoFiftySingleFamily = prop.TwoFiftySingleFamily.GetValueOrDefault(),
            //    Units = prop.Units.GetValueOrDefault(),
            //    Walkscore = prop.Walkscore.GetValueOrDefault(),
            //    YearBuilt = prop.YearBuilt.GetValueOrDefault(),
            //    BuildingEnclosure = Utils.Convert<BuildingEnclosure>(_context, prop.BuildingEnclosureId, Utils.GetDescription),
            //    CommonAreas = Utils.Convert<CommonCode>(_context, prop.commonAreas, Utils.GetDescription),
            //    NeighborhoodCondition = Utils.Convert<NeighborhoodCode>(_context, prop.neighCondition, Utils.GetDescription),
            //    SocioEcon = Utils.Convert<SocioEconCode>(_context, prop.socioEcon, Utils.GetDescription),
            //    StreetConnectivity = Utils.Convert<StreetconnCode>(_context, prop.streetConn, Utils.GetDescription),
            //    StreetSafety = Utils.Convert<StreetSafteyCode>(_context, prop.streetSaftey, Utils.GetDescription),
            //    StreetType = Utils.Convert<StreetType>(_context, prop.streetCode, Utils.GetDescription),
            //    StreetWalkability = Utils.Convert<StreetwalkCode>(_context, prop.streetWalk, Utils.GetDescription),
            //    Type = Utils.Convert<PropertyType>(_context, prop.typeCode, Utils.GetDescription),
            //    Notes = prop.notes,
            //    PictureMetaData = null, // TODO
            //    Score = 0 // TODO
            //};
            throw new NotImplementedException();
        }

        public AdminPropertyDTO FindAdminProperty(int id)
        {
            //var prop = _propRepo.Get(id);
            //return new AdminPropertyDTO
            //{
            //    Id = prop.id,
            //    Address = Convert(prop.Address),
            //    AdminNotes = prop.adminNotes,
            //    Area = prop.area.GetValueOrDefault(),
            //    Density = prop.density.GetValueOrDefault(),
            //    TwoFiftyAppartments = prop.twoFiftyApts.GetValueOrDefault(),
            //    TwoFiftySingleFamily = prop.twoFiftySingleFam.GetValueOrDefault(),
            //    Units = prop.units.GetValueOrDefault(),
            //    Walkscore = prop.walkscore.GetValueOrDefault(),
            //    YearBuilt = prop.yearBuilt.GetValueOrDefault(),
            //    BuildingEnclosure = prop.buildingEnclosure.GetValueOrDefault(),
            //    CommonAreas = prop.commonAreas.GetValueOrDefault(),
            //    NeighborhoodCondition = prop.neighCondition.GetValueOrDefault(),
            //    SocioEcon = prop.socioEcon.GetValueOrDefault(),
            //    StreetConnectivity = prop.streetConn.GetValueOrDefault(),
            //    StreetSafety = prop.streetSaftey.GetValueOrDefault(),
            //    StreetType = prop.streetCode.GetValueOrDefault(),
            //    StreetWalkability = prop.streetWalk.GetValueOrDefault(),
            //    Type = prop.typeCode.GetValueOrDefault(),
            //    Notes = prop.notes,
            //    NotFinished = prop.notFinished,
            //    WalkscoreNotes = prop.walkscoreNotes,
            //    PictureMetaData = null // TODO
            //};
            throw new NotImplementedException();
        }

        public List<CsvPropertyDTO> FindAllCsvProperties()
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<int, int> FindShowcaseProperty()
        {
            //try
            //{
            //    var count = _db.GetCollection<Picture>(typeof(Picture).Name)
            //        .Count(Query<Picture>.EQ(p => p.frontPage, (short?)1));
            //    var index = new Random().Next((int)count);
            //    var picture = _db.GetCollection<Picture>(typeof(Picture).Name)
            //        .FindAs<Picture>(Query<Picture>.EQ(p => p.frontPage, (short?)1))
            //        .Skip(index)
            //        .First();
            //    return new KeyValuePair<int, int>(picture.property_id.Value, picture.id);
            //}
            //catch (Exception)
            //{
            //    return default(KeyValuePair<int, int>);
            //}
            throw new NotImplementedException();
        }

        private AddressDTO Convert(Address a)
        {
            return new AddressDTO
            {
                Street1 = a.Street1,
                Street2 = a.Street2,
                State = a.State,
                City = a.City,
                Zip = a.Zip,
                //Country = a.Country
            };
        }

        //private List<PictureMetaData> GetPictureMetaData(Property prop)
        //{
        //    var pictures = _db.GetCollection<Picture>(typeof(Picture).Name)
        //        .FindAs<Picture>(Query<Picture>.EQ(p => p.property_id, prop.id));
        //    //return (from prop in _context.Properties
        //    //        join pic in _context.Pictures on prop.id equals pic.property_id
        //    //        where prop.id == propertyNumber
        //    //        select new PictureMetaData
        //    //        {
        //    //            PictureId = pic.id,
        //    //            PrimaryPicture = pic.mainPicture == 1,
        //    //            SecondaryPicture = pic.secondaryPicture == 1,
        //    //            FrontPage = pic.frontPage == 1
        //    //        })
        //    //        .OrderByDescending(md => md.PrimaryPicture)
        //    //        .ToList();
        //}

        //private string Convert<T>(int? key, Func<T, string> getDescription)
        //{
        //    var item = _db.GetCollection<T>(typeof(T).Name).FindOneById(key);
        //    return getDescription(item);
        //}

        //private string GetDescription(CommonCode code) { return code.description; }
        //private string GetDescription(EnclosureCode code) { return code.description; }
        //private string GetDescription(NeighborhoodCode code) { return code.description; }
        //private string GetDescription(SocioEconCode code) { return code.description; }
        //private string GetDescription(StreetconnCode code) { return code.description; }
        //private string GetDescription(StreetSafteyCode code) { return code.description; }
        //private string GetDescription(StreetType code) { return code.description; }
        //private string GetDescription(StreetwalkCode code) { return code.description; }
        //private string GetDescription(PropertyType code) { return code.description; }

    }
}
