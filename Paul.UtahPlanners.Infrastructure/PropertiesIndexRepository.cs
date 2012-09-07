using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain;
using System.Data.Entity;

namespace UtahPlanners.Infrastructure
{
    public class PropertiesIndexRepository : IPropertiesIndexRepository
    {
        private PropertiesDB _context;

        public PropertiesIndexRepository(PropertiesDB context)
        {
            _context = context;
        }

        #region IPropertiesIndexRepository Members

        public List<PropertiesIndex> GetIndecies(IndexFilter filter, IndexSort sort)
        {
            // Filter - or not
            var props = filter != null ? GetFilteredIndexRecords(filter) 
                : _context.PropertiesIndexes.AsQueryable();
            
            // And sort - or not
            if (sort != null)
                props = PerformSort(sort, props);
            
            return props.ToList();
        }

        #endregion

        private IQueryable<PropertiesIndex> GetFilteredIndexRecords(IndexFilter filter)
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

        private IQueryable<PropertiesIndex> PerformSort(IndexSort sort, IQueryable<PropertiesIndex> props)
        {
            switch (sort.Direction)
            {
                case Direction.Descending:
                    props = SortDescending(sort.Column, props);
                    break;
                case Direction.Ascending:
                default:
                    props = SortAscending(sort.Column, props);
                    break;
            }
            return props;
        }

        private IQueryable<PropertiesIndex> SortAscending(IndexColumn column, IQueryable<PropertiesIndex> props)
        {
            switch (column)
            {
                case IndexColumn.Id:
                    props = props.OrderBy(p => p.id);
                    break;
                case IndexColumn.City:
                    props = props.OrderBy(p => p.city);
                    break;
                case IndexColumn.Score:
                    props = props.OrderBy(p => p.OverallScore);
                    break;
                case IndexColumn.Type:
                    props = props.OrderBy(p => p.PropertyTypeDescription);
                    break;
                case IndexColumn.Density:
                    props = props.OrderBy(p => p.density);
                    break;
                case IndexColumn.Units:
                    props = props.OrderBy(p => p.units);
                    break;
                case IndexColumn.Year:
                    props = props.OrderBy(p => p.yearBuilt);
                    break;
                case IndexColumn.StreetType:
                    props = props.OrderBy(p => p.StreetTypeDescription);
                    break;
                case IndexColumn.Walkability:
                    props = props.OrderBy(p => p.StreetWalkDescription);
                    break;
                case IndexColumn.Walkscore:
                    props = props.OrderBy(p => p.walkscore);
                    break;
                case IndexColumn.SocioEcon:
                    props = props.OrderBy(p => p.SocioEconDescription);
                    break;
                case IndexColumn.TwoFiftySF:
                    props = props.OrderBy(p => p.twoFiftySingleFam);
                    break;
            }
            return props;
        }

        private IQueryable<PropertiesIndex> SortDescending(IndexColumn column, IQueryable<PropertiesIndex> props)
        {
            switch (column)
            {
                case IndexColumn.Id:
                    props = props.OrderByDescending(p => p.id);
                    break;
                case IndexColumn.City:
                    props = props.OrderByDescending(p => p.city);
                    break;
                case IndexColumn.Score:
                    props = props.OrderByDescending(p => p.OverallScore);
                    break;
                case IndexColumn.Type:
                    props = props.OrderByDescending(p => p.PropertyTypeDescription);
                    break;
                case IndexColumn.Density:
                    props = props.OrderByDescending(p => p.density);
                    break;
                case IndexColumn.Units:
                    props = props.OrderByDescending(p => p.units);
                    break;
                case IndexColumn.Year:
                    props = props.OrderByDescending(p => p.yearBuilt);
                    break;
                case IndexColumn.StreetType:
                    props = props.OrderByDescending(p => p.StreetTypeDescription);
                    break;
                case IndexColumn.Walkability:
                    props = props.OrderByDescending(p => p.StreetWalkDescription);
                    break;
                case IndexColumn.Walkscore:
                    props = props.OrderByDescending(p => p.walkscore);
                    break;
                case IndexColumn.SocioEcon:
                    props = props.OrderByDescending(p => p.SocioEconDescription);
                    break;
                case IndexColumn.TwoFiftySF:
                    props = props.OrderByDescending(p => p.twoFiftySingleFam);
                    break;
            }
            return props;
        }
    }
}
