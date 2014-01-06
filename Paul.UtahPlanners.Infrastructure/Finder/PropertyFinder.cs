using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Contract.Finder;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Domain.DTO;
using UtahPlanners.Domain.Contract.Service;

namespace UtahPlanners.Infrastructure.Finder
{
    public class PropertyFinder : IPropertyFinder
    {
        private PropertyContext _context;

        public PropertyFinder(PropertyContext context)
        {
            _context = context;
        }

        #region IPropertyFinder Members

        public UserPropertyDTO FindProperty(Guid id, Weights weights)
        {
            //var dto = (from p in _context.Properties
            //           join pt in _context.PropertyTypeLookups on p.PropertyTypeId equals pt.Id
            //           join ssc in _context.StreetSafetyLookups on p.StreetSafetyId equals ssc.Id
            //           join ec in _context.BuildingEnclosureLookups on p.BuildingEnclosureId equals ec.Id
            //           join cc in _context.CommonAreaLookups on p.CommonAreasId equals cc.Id
            //           join scc in _context.StreetConnectivityLookups on p.StreetConnectivityId equals scc.Id
            //           join swc in _context.StreetWalkabilityLookups on p.StreetWalkabilityId equals swc.Id
            //           join nc in _context.NeighborhoodConditionLookups on p.NeighborhoodConditionId equals nc.Id
            //           join st in _context.StreetTypeLookups on p.StreetTypeId equals st.Id
            //           join sec in _context.SocioEconLookups on p.SocioEconCodeId equals sec.Id
            //           where p.Id == id
            //            select new UserPropertyDTO
            //            {
            //                Id = p.Id,
            //                Type = pt.Description,
            //                Score =
            //                (
            //                    (int)
            //                    (((nc.weight.Value / 6.0) * weights.neighCondition.Value) +
            //                    ((swc.weight.Value / 20.0) * weights.streetWalk.Value) +
            //                    ((cc.weight.Value / 15.0) * weights.commonAreas.Value) +
            //                    ((scc.weight.Value / 6.0) * weights.streetConn.Value) +
            //                    ((ec.weight.Value / 4.0) * weights.buildingEnclosure.Value) +
            //                    ((ssc.weight.Value / 10.0) * weights.streetSaftey.Value) +
            //                    ((p.walkscore.Value / 100.0) * weights.walkscore.Value) +
            //                    (((p.twoFiftySingleFam.Value > 35 ? 15 :
            //                        p.twoFiftySingleFam.Value > 25 ? 13 :
            //                        p.twoFiftySingleFam.Value > 15 ? 11 :
            //                        p.twoFiftySingleFam.Value > 10 ? 9 :
            //                        p.twoFiftySingleFam.Value > 6 ? 7 :
            //                        p.twoFiftySingleFam.Value > 3 ? 4 :
            //                        p.twoFiftySingleFam.Value > 0 ? 2 : 0) / 15.0) * weights.twoFiftySingleFam.Value) +
            //                    (((p.twoFiftyApts.Value > 30 ? 5 :
            //                        p.twoFiftyApts.Value > 10 ? 4 :
            //                        p.twoFiftyApts.Value > 0 ? 2 : 0) / 5.0) * weights.twoFiftyApts.Value))
            //                ),
            //                StreetSafety = ssc.description,
            //                BuildingEnclosure = ec.description,
            //                CommonAreas = cc.description,
            //                StreetConnectivity = scc.description,
            //                StreetWalkability = swc.description,
            //                Walkscore = p.walkscore.Value,
            //                NeighborhoodCondition = nc.description,
            //                TwoFiftySingleFamily = p.twoFiftySingleFam.Value,
            //                TwoFiftyAppartments = p.twoFiftyApts.Value,
            //                Address = new AddressDTO
            //                {
            //                    Street1 = a.street1,
            //                    Street2 = a.street2,
            //                    City = a.city,
            //                    State = a.state,
            //                    Zip = a.zip,
            //                    Country = a.country
            //                },
            //                Density = p.density.Value,
            //                Area = p.area.Value,
            //                Units = p.units.Value,
            //                StreetType = st.description,
            //                YearBuilt = p.yearBuilt.Value,
            //                SocioEcon = sec.description,
            //                Notes = p.notes,
            //            })
            //           .FirstOrDefault();
            //dto.PictureMetaData = GetPictureMetaData(id);
            //return dto;
            throw new NotImplementedException();
        }

        public AdminPropertyDTO FindAdminProperty(int id)
        {
            //var dto = (from p in _context.Properties
            //           join a in _context.Addresses on p.Address.id equals a.id
            //           where p.id == id
            //           select new AdminPropertyDTO
            //           {
            //               Id = p.id,
            //               Address = new AddressDTO
            //               {
            //                   Street1 = a.street1,
            //                   Street2 = a.street2,
            //                   City = a.city,
            //                   State = a.state,
            //                   Zip = a.zip,
            //                   Country = a.country
            //               },
            //               Density = p.density.Value,
            //               Area = p.area.Value,
            //               Units = p.units.Value,
            //               YearBuilt = p.yearBuilt.Value,
            //               Walkscore = p.walkscore.Value,
            //               TwoFiftySingleFamily = p.twoFiftySingleFam.Value,
            //               TwoFiftyAppartments = p.twoFiftyApts.Value,
            //               Type = p.typeCode.Value,
            //               StreetType = p.streetCode.Value,
            //               SocioEcon = p.socioEcon.Value,
            //               StreetSafety = p.streetSaftey.Value,
            //               BuildingEnclosure = p.buildingEnclosure.Value,
            //               CommonAreas = p.commonAreas.Value,
            //               StreetConnectivity = p.streetConn.Value,
            //               StreetWalkability = p.streetWalk.Value,
            //               NeighborhoodCondition = p.neighCondition.Value,
            //               Notes = p.notes,
            //               AdminNotes = p.adminNotes,
            //               WalkscoreNotes = p.walkscoreNotes,
            //               NotFinished = p.notFinished
            //           })
            //           .FirstOrDefault();
            //dto.PictureMetaData = GetPictureMetaData(id);
            //return dto;
            throw new NotImplementedException();
        }

        public List<CsvPropertyDTO> FindAllCsvProperties()
        {
            //var props = (from p in _context.Properties
            //             join a in _context.Addresses on p.Address.id equals a.id
            //             select new CsvPropertyDTO
            //             {
            //                 Id = p.id,
            //                 PropertyType = p.PropertyType.description,
            //                 StreetType = p.StreetType.description,
            //                 SocioEconType = p.SocioEconCode.description,
            //                 StreetSafetyCode = p.StreetSafteyCode.description,
            //                 BuildingEnclosureCode = p.EnclosureCode.description,
            //                 CommonAreasCode = p.CommonCode.description,
            //                 StreetConnectivityCode = p.StreetconnCode.description,
            //                 StreetWalkabilityCode = p.StreetwalkCode.description,
            //                 NeighborhoodConditionCode = p.NeighborhoodCode.description,
            //                 Address = new AddressDTO
            //                 {
            //                     Street1 = a.street1,
            //                     Street2 = a.street2,
            //                     City = a.city,
            //                     State = a.state,
            //                     Zip = a.zip,
            //                     Country = a.country
            //                 },
            //                 Walkscore = p.walkscore.HasValue ? p.walkscore.Value : 0,
            //                 TwoFiftySingleFamily = p.twoFiftySingleFam.HasValue ? p.twoFiftySingleFam.Value : 0,
            //                 TwoFiftyAppartments = p.twoFiftyApts.HasValue ? p.twoFiftyApts.Value : 0,
            //                 Density = p.density.HasValue ? p.density.Value : 0,
            //                 Area = p.area.HasValue ? p.area.Value : 0,
            //                 Units = p.units.HasValue ? p.units.Value : 0,
            //                 YearBuilt = p.yearBuilt.HasValue ? p.yearBuilt.Value : 0
            //             }).ToList();
            //return props;
            throw new NotImplementedException();
        }

        public KeyValuePair<int, int> FindShowcaseProperty()
        {
            //var count = _context.Pictures.Where(p => p.frontPage == 1)
            //    .Count();

            //var index = new Random().Next(count);

            //var result = _context.Pictures.Where(p => p.frontPage == 1)
            //    .OrderBy(p => p.id)
            //    .Skip(index)
            //    .Select(p => new { PropertyId = p.property_id.Value, PictureId = p.id })
            //    .FirstOrDefault();
            //return new KeyValuePair<int, int>(result.PropertyId, result.PictureId);
            throw new NotImplementedException();
        }

        #endregion

        private List<PictureMetaData> GetPictureMetaData(int propertyNumber)
        {
            //return (from prop in _context.Properties
            //        join pic in _context.Pictures on prop.id equals pic.property_id
            //        where prop.id == propertyNumber
            //        select new PictureMetaData
            //        {
            //            PictureId = pic.id,
            //            PrimaryPicture = pic.mainPicture == 1,
            //            SecondaryPicture = pic.secondaryPicture == 1,
            //            FrontPage = pic.frontPage == 1
            //        })
            //        .OrderByDescending(md => md.PrimaryPicture)
            //        .ToList();
            throw new NotImplementedException();
        }
    }
}
