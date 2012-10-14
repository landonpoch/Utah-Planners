using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Infrastructure.Shared;
using UtahPlanners.Domain.Contract.Finder;
using UtahPlanners.Domain.DTO;

namespace UtahPlanners.Infrastructure.Finder
{
    public class PropertiesIndexFinder : IPropertiesIndexFinder
    {
        private PropertiesDB _context;

        public PropertiesIndexFinder(PropertiesDB context)
        {
            _context = context;
        }

        #region IPropertiesIndexRepository Members

        public List<PropertiesIndex> FindIndecies(IndexFilter filter, IndexSort sort)
        {
            // Filter - or not
            var props = filter != null ? FindFilteredIndexRecords(filter) 
                : _context.PropertiesIndexes.AsQueryable();
            
            return props.Sort(sort).ToList();
        }

        public List<AdminPropertyIndexDTO> FindAdminIndecies(PropertySort sort)
        {
            var indicies = from p in _context.Properties
                           join a in _context.Addresses on p.Address.id equals a.id
                           join pt in _context.PropertyTypes on p.PropertyType.id equals pt.id
                           select new AdminPropertyIndexDTO
                           {
                               Id = p.id,
                               City = a.city,
                               Description = pt.description,
                               Density = p.density.Value,
                               Units = p.units.Value,
                               YearBuilt = p.yearBuilt.Value,
                               AdminNotes = p.adminNotes,
                               NotFinished = p.notFinished
                           };
            indicies = indicies.Sort(sort);
            return indicies.ToList();
        }

        #endregion

        private IQueryable<PropertiesIndex> FindFilteredIndexRecords(IndexFilter filter)
        {
            var props = _context.Properties.AsQueryable().Include(p => p.Address);
            if (filter.PropertyId.HasValue)
                props = props.Where(p => p.id == filter.PropertyId.Value);
            if (!String.IsNullOrEmpty(filter.City))
                props = props.Where(p => p.Address != null && p.Address.city.Contains(filter.City));
            if (filter.PropertyTypes != null 
                && filter.PropertyTypes.Count > 0)
                props = props.Where(p => p.typeCode.HasValue && filter.PropertyTypes.Contains(p.typeCode.Value));
            if (filter.DensityRange != null)
            {
                if (filter.DensityRange.LowValue.HasValue)
                    props = props.Where(p => p.density.HasValue && p.density > filter.DensityRange.LowValue.Value);
                if (filter.DensityRange.HighValue.HasValue)
                    props = props.Where(p => p.density.HasValue && p.density < filter.DensityRange.HighValue.Value);
            }
            if (filter.AreaRange != null)
            {
                if (filter.AreaRange.LowValue.HasValue)
                    props = props.Where(p => p.area.HasValue && p.area.Value > filter.AreaRange.LowValue.Value);
                if (filter.AreaRange.HighValue.HasValue)
                    props = props.Where(p => p.area.HasValue && p.area.Value < filter.AreaRange.HighValue.Value);
            }
            if (filter.UnitRange != null)
            {
                if (filter.UnitRange.LowValue.HasValue)
                    props = props.Where(p => p.units.HasValue && p.units > filter.UnitRange.LowValue.Value);
                if (filter.UnitRange.HighValue.HasValue)
                    props = props.Where(p => p.units.HasValue && p.units < filter.UnitRange.HighValue.Value);
            }
            if (filter.StreetType.HasValue)
                props = props.Where(p => p.StreetType.id == filter.StreetType.Value);
            if (filter.YearBuiltRange != null)
            {
                if (filter.YearBuiltRange.LowValue.HasValue)
                    props = props.Where(p => p.yearBuilt.HasValue && p.yearBuilt.Value > filter.YearBuiltRange.LowValue.Value);
                if (filter.YearBuiltRange.HighValue.HasValue)
                    props = props.Where(p => p.yearBuilt.HasValue && p.yearBuilt.Value < filter.YearBuiltRange.HighValue.Value);
            }
            if (filter.SocioEconType.HasValue)
                props = props.Where(p => p.socioEcon.HasValue && p.socioEcon.Value == filter.SocioEconType.Value);
            if (filter.StreetSafetyType.HasValue)
                props = props.Where(p => p.streetSaftey.HasValue && p.streetSaftey.Value == filter.StreetSafetyType.Value);
            if (filter.BuildingEnclosureType.HasValue)
                props = props.Where(p => p.buildingEnclosure.HasValue && p.buildingEnclosure.Value == filter.BuildingEnclosureType.Value);
            if (filter.CommonAreasType.HasValue)
                props = props.Where(p => p.commonAreas.HasValue && p.commonAreas.Value == filter.CommonAreasType.Value);
            if (filter.StreetConnectivityType.HasValue)
                props = props.Where(p => p.streetConn.HasValue && p.streetConn.Value == filter.StreetConnectivityType.Value);
            if (filter.StreetWalkabilityType.HasValue)
                props = props.Where(p => p.streetWalk.HasValue && p.streetWalk.Value == filter.StreetWalkabilityType.Value);
            if (filter.WalkscoreRange != null)
            {
                if (filter.WalkscoreRange.LowValue.HasValue)
                    props = props.Where(p => p.walkscore.HasValue && p.walkscore.Value > filter.WalkscoreRange.LowValue.Value);
                if (filter.WalkscoreRange.HighValue.HasValue)
                    props = props.Where(p => p.walkscore.HasValue && p.walkscore.Value < filter.WalkscoreRange.HighValue.Value);
            }
            if (filter.NeighborhoodConditionType.HasValue)
                props = props.Where(p => p.neighCondition.HasValue && p.neighCondition.Value == filter.NeighborhoodConditionType.Value);
            if (filter.TwoFiftySingleFamilyRange != null)
            {
                if (filter.TwoFiftySingleFamilyRange.LowValue.HasValue)
                    props = props.Where(p => p.twoFiftySingleFam.HasValue 
                        && p.twoFiftySingleFam.Value > filter.TwoFiftySingleFamilyRange.LowValue.Value);
                if (filter.TwoFiftySingleFamilyRange.HighValue.HasValue)
                    props = props.Where(p => p.twoFiftySingleFam.HasValue
                        && p.twoFiftySingleFam.Value < filter.TwoFiftySingleFamilyRange.HighValue.Value);
            }
            if (filter.TwoFiftyApartmentsRange != null)
            {
                if (filter.TwoFiftyApartmentsRange.LowValue.HasValue)
                    props = props.Where(p => p.twoFiftyApts.HasValue
                        && p.twoFiftyApts.Value > filter.TwoFiftyApartmentsRange.LowValue.Value);
                if (filter.TwoFiftyApartmentsRange.HighValue.HasValue)
                    props = props.Where(p => p.twoFiftyApts.HasValue
                        && p.twoFiftyApts.Value < filter.TwoFiftyApartmentsRange.HighValue.Value);
            }

            var indecies = (from p in props
                            join i in _context.PropertiesIndexes on p.id equals i.id
                            select i);

            // This must be filtered on the index view because the field doesn't exist on the properties table
            if (filter.ScoreRange != null)
            {
                if (filter.ScoreRange.LowValue.HasValue)
                    indecies = indecies.Where(i => i.OverallScore.HasValue
                        && i.OverallScore.Value > filter.ScoreRange.LowValue.Value);
                if (filter.ScoreRange.HighValue.HasValue)
                    indecies = indecies.Where(i => i.OverallScore.HasValue
                        && i.OverallScore.Value < filter.ScoreRange.HighValue.Value);
            }

            return indecies;
        }
    }

    public static class PropertiesIndexExtensions
    {
        public static IQueryable<PropertiesIndex> Sort(this IQueryable<PropertiesIndex> props, IndexSort sort)
        {
            if (sort != null)
            {
                var column = sort.Column;
                var direction = sort.Direction;
                switch (column)
                {
                    case IndexColumn.Id:
                        props = props.Sort(p => p.id, direction);
                        break;
                    case IndexColumn.City:
                        props = props.Sort(p => p.city, direction);
                        break;
                    case IndexColumn.Score:
                        props = props.Sort(p => p.OverallScore, direction);
                        break;
                    case IndexColumn.Type:
                        props = props.Sort(p => p.PropertyTypeDescription, direction);
                        break;
                    case IndexColumn.Density:
                        props = props.Sort(p => p.density, direction);
                        break;
                    case IndexColumn.Units:
                        props = props.Sort(p => p.units, direction);
                        break;
                    case IndexColumn.Year:
                        props = props.Sort(p => p.yearBuilt, direction);
                        break;
                    case IndexColumn.StreetType:
                        props = props.Sort(p => p.StreetTypeDescription, direction);
                        break;
                    case IndexColumn.Walkability:
                        props = props.Sort(p => p.StreetWalkDescription, direction);
                        break;
                    case IndexColumn.Walkscore:
                        props = props.Sort(p => p.walkscore, direction);
                        break;
                    case IndexColumn.SocioEcon:
                        props = props.Sort(p => p.SocioEconDescription, direction);
                        break;
                    case IndexColumn.TwoFiftySF:
                        props = props.Sort(p => p.twoFiftySingleFam, direction);
                        break;
                }
            }
            return props;
        }

        public static IQueryable<AdminPropertyIndexDTO> Sort(this IQueryable<AdminPropertyIndexDTO> props, PropertySort sort)
        {
            if (sort != null)
            {
                var column = sort.Column;
                var direction = sort.Direction;
                switch (column)
                {
                    case PropertyColumn.Id:
                        props = props.Sort(p => p.Id, direction);
                        break;
                    case PropertyColumn.City:
                        props = props.Sort(p => p.City, direction);
                        break;
                    case PropertyColumn.Description:
                        props = props.Sort(p => p.Description, direction);
                        break;
                    case PropertyColumn.Density:
                        props = props.Sort(p => p.Density, direction);
                        break;
                    case PropertyColumn.Units:
                        props = props.Sort(p => p.Units, direction);
                        break;
                    case PropertyColumn.YearBuilt:
                        props = props.Sort(p => p.YearBuilt, direction);
                        break;
                    case PropertyColumn.AdminNotes:
                        props = props.Sort(p => p.AdminNotes, direction);
                        break;
                    case PropertyColumn.NotFinished:
                        props = props.Sort(p => p.NotFinished, direction);
                        break;
                }
            }
            return props;
        }
    }
}
