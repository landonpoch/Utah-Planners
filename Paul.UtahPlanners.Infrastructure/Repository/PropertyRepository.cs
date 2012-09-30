using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Infrastructure.Shared;

namespace UtahPlanners.Infrastructure.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private PropertiesDB _context;
        private IConfigSettings _settings;

        public PropertyRepository(PropertiesDB context, IConfigSettings settings)
        {
            _context = context;
            _settings = settings;
        }

        #region IPropertyRepository Members

        public void Add(Property property)
        {
            _context.Properties.Add(property);
        }

        public List<Property> GetAllProperties(PropertySort sort)
        {
            var properties = GetPropertiesQuery().Sort(sort).ToList();
            AppendMetaData(properties);
            return properties;
        }

        public Property Get(int id)
        {
            // TODO: Try to do this in 1 DB hit
            var property = GetPropertiesQuery()
                .FirstOrDefault(p => p.id == id); // DB Hit 1

            property.Weights = _settings.Weights;
            var pictureMetaData = (from prop in _context.Properties
                              join pic in _context.Pictures on prop.id equals pic.property_id
                              where prop.id == id
                              select new PictureMetaData
                              {
                                  PictureId = pic.id,
                                  PrimaryPicture = pic.mainPicture == 1,
                                  SecondaryPicture = pic.secondaryPicture == 1,
                                  FrontPage = pic.frontPage == 1
                              })
                              .ToList(); // DB Hit 2
            
            property.PictureMetaData = pictureMetaData
                .OrderByDescending(p => p.PrimaryPicture) // Put the main picture first
                .ToList();

            // TODO: See if there is a better way to do this.  I don't think the repository should call methods on the domain entity
            property.CalculateScore();
            
            return property;
        }

        public void Remove(Property property)
        {
            _context.Properties.Remove(property);
        }

        public KeyValuePair<int, int> GetShowcaseProperty()
        {
            var count = _context.Pictures.Where(p => p.frontPage == 1)
                .Count();

            var index = new Random().Next(count);

            var result = _context.Pictures.Where(p => p.frontPage == 1)
                .OrderBy(p => p.id)
                .Skip(index)
                .Select(p => new { PropertyId = p.property_id.Value, PictureId = p.id })
                .FirstOrDefault();
            return new KeyValuePair<int, int>(result.PropertyId, result.PictureId);
        }

        #endregion

        private IQueryable<Property> GetPropertiesQuery()
        {
            return _context.Properties
                .Include(p => p.Address)
                .Include(p => p.CommonCode)
                .Include(p => p.EnclosureCode)
                .Include(p => p.NeighborhoodCode)
                .Include(p => p.PropertyType)
                .Include(p => p.SocioEconCode)
                .Include(p => p.StreetconnCode)
                .Include(p => p.StreetSafteyCode)
                .Include(p => p.StreetType)
                .Include(p => p.StreetwalkCode);
        }

        private void AppendMetaData(List<Property> properties)
        {
            var propertyIds = properties.Select(p => p.id).ToList();
            var metaData = (from md in _context.Pictures
                            where propertyIds.Contains(md.property_id.Value)
                            select new PictureMetaData
                            {
                                PictureId = md.id,
                                PrimaryPicture = md.mainPicture == 1,
                                SecondaryPicture = md.secondaryPicture == 1,
                                FrontPage = md.frontPage == 1,
                                PropertyId = md.property_id.Value
                            })
                           .ToList();

            properties.ForEach(p =>
            {
                p.PictureMetaData = metaData.Where(md => md.PropertyId == p.id).ToList();
            });
        }
    }

    public static class PropertyExtensions
    {
        public static IQueryable<Property> Sort(this IQueryable<Property> props, PropertySort sort)
        {
            if (sort != null)
            {
                Direction direction = sort.Direction;
                switch (sort.Column)
                {
                    case PropertyColumn.Id:
                        props = props.Sort(p => p.id, direction);
                        break;
                    case PropertyColumn.City:
                        props = props.Sort(p => p.Address.city, direction);
                        break;
                    case PropertyColumn.Description:
                        props = props.Sort(p => p.PropertyType.description, direction);
                        break;
                    case PropertyColumn.Density:
                        props = props.Sort(p => p.density, direction);
                        break;
                    case PropertyColumn.Units:
                        props = props.Sort(p => p.units, direction);
                        break;
                    case PropertyColumn.YearBuilt:
                        props = props.Sort(p => p.yearBuilt, direction);
                        break;
                    case PropertyColumn.AdminNotes:
                        props = props.Sort(p => p.adminNotes, direction);
                        break;
                    case PropertyColumn.NotFinished:
                        props = props.Sort(p => p.notFinished, direction);
                        break;
                }
            }
            return props;
        }
    }
}
