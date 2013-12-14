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
using UtahPlanners.Infrastructure.Shared;

namespace UtahPlanners.Infrastructure.Finder.Mongo
{
    public class MongoPropertyFinder : IPropertyFinder
    {
        private MongoDatabase _db;
        private IPropertyRepository _propRepo;

        public MongoPropertyFinder(MongoDatabase db, IPropertyRepository propRepo)
        {
            _db = db;
            _propRepo = propRepo;
        }

        public UserPropertyDTO FindProperty(int id, Weight weights)
        {
            var prop = _propRepo.Get(id);
            return new UserPropertyDTO
            {
                Id = prop.id,
                Address = Convert(prop.Address),
                Area = prop.area.GetValueOrDefault(),
                Density = prop.density.GetValueOrDefault(),
                TwoFiftyAppartments = prop.twoFiftyApts.GetValueOrDefault(),
                TwoFiftySingleFamily = prop.twoFiftySingleFam.GetValueOrDefault(),
                Units = prop.units.GetValueOrDefault(),
                Walkscore = prop.walkscore.GetValueOrDefault(),
                YearBuilt = prop.yearBuilt.GetValueOrDefault(),
                BuildingEnclosure = Utils.Convert<EnclosureCode>(_db, prop.buildingEnclosure, Utils.GetDescription),
                CommonAreas = Utils.Convert<CommonCode>(_db, prop.commonAreas, Utils.GetDescription),
                NeighborhoodCondition = Utils.Convert<NeighborhoodCode>(_db, prop.neighCondition, Utils.GetDescription),
                SocioEcon = Utils.Convert<SocioEconCode>(_db, prop.socioEcon, Utils.GetDescription),
                StreetConnectivity = Utils.Convert<StreetconnCode>(_db, prop.streetConn, Utils.GetDescription),
                StreetSafety = Utils.Convert<StreetSafteyCode>(_db, prop.streetSaftey, Utils.GetDescription),
                StreetType = Utils.Convert<StreetType>(_db, prop.streetCode, Utils.GetDescription),
                StreetWalkability = Utils.Convert<StreetwalkCode>(_db, prop.streetWalk, Utils.GetDescription),
                Type = Utils.Convert<PropertyType>(_db, prop.typeCode, Utils.GetDescription),
                Notes = prop.notes,
                PictureMetaData = null, // TODO
                Score = 0 // TODO
            };
        }

        public AdminPropertyDTO FindAdminProperty(int id)
        {
            var prop = _propRepo.Get(id);
            return new AdminPropertyDTO
            {
                Id = prop.id,
                Address = Convert(prop.Address),
                AdminNotes = prop.adminNotes,
                Area = prop.area.GetValueOrDefault(),
                Density = prop.density.GetValueOrDefault(),
                TwoFiftyAppartments = prop.twoFiftyApts.GetValueOrDefault(),
                TwoFiftySingleFamily = prop.twoFiftySingleFam.GetValueOrDefault(),
                Units = prop.units.GetValueOrDefault(),
                Walkscore = prop.walkscore.GetValueOrDefault(),
                YearBuilt = prop.yearBuilt.GetValueOrDefault(),
                BuildingEnclosure = prop.buildingEnclosure.GetValueOrDefault(),
                CommonAreas = prop.commonAreas.GetValueOrDefault(),
                NeighborhoodCondition = prop.neighCondition.GetValueOrDefault(),
                SocioEcon = prop.socioEcon.GetValueOrDefault(),
                StreetConnectivity = prop.streetConn.GetValueOrDefault(),
                StreetSafety = prop.streetSaftey.GetValueOrDefault(),
                StreetType = prop.streetCode.GetValueOrDefault(),
                StreetWalkability = prop.streetWalk.GetValueOrDefault(),
                Type = prop.typeCode.GetValueOrDefault(),
                Notes = prop.notes,
                NotFinished = prop.notFinished,
                WalkscoreNotes = prop.walkscoreNotes,
                PictureMetaData = null // TODO
            };
        }

        public List<CsvPropertyDTO> FindAllCsvProperties()
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<int, int> FindShowcaseProperty()
        {
            try
            {
                var count = _db.GetCollection<Picture>(typeof(Picture).Name)
                    .Count(Query<Picture>.EQ(p => p.frontPage, (short?)1));
                var index = new Random().Next((int)count);
                var picture = _db.GetCollection<Picture>(typeof(Picture).Name)
                    .FindAs<Picture>(Query<Picture>.EQ(p => p.frontPage, (short?)1))
                    .Skip(index)
                    .First();
                return new KeyValuePair<int, int>(picture.property_id.Value, picture.id);
            }
            catch (Exception)
            {
                return default(KeyValuePair<int, int>);
            }
        }

        private AddressDTO Convert(Address a)
        {
            return new AddressDTO
            {
                Street1 = a.street1,
                Street2 = a.street2,
                State = a.state,
                City = a.city,
                Zip = a.zip,
                Country = a.country
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
