﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Infrastructure.DAO;

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

        public Property Get(int id)
        {
            // TODO: Try to do this in 1 DB hit
            var property = _context.Properties
                .Include(p => p.Address)
                .Include(p => p.CommonCode)
                .Include(p => p.EnclosureCode)
                .Include(p => p.NeighborhoodCode)
                .Include(p => p.PropertyType)
                .Include(p => p.SocioEconCode)
                .Include(p => p.StreetconnCode)
                .Include(p => p.StreetSafteyCode)
                .Include(p => p.StreetType)
                .Include(p => p.StreetwalkCode)
                .FirstOrDefault(p => p.id == id); // DB Hit 1

            property.Weights = _settings.Weights;
            var pictureIds = (from prop in _context.Properties
                              join pic in _context.Pictures on prop.id equals pic.property_id
                              where prop.id == id
                              select new
                              {
                                  PictureId = pic.id,
                                  IsMainPicture = pic.mainPicture == 1,
                                  IsSecondaryPicture = pic.secondaryPicture == 1
                              })
                              .ToList(); // DB Hit 2
            
            property.PictureIds = pictureIds
                .Where(p => !p.IsSecondaryPicture)
                .OrderByDescending(p => p.IsMainPicture) // Put the main picture first
                .Select(p => p.PictureId)
                .ToList();

            property.SecondaryPictureId = pictureIds
                .Where(p => p.IsSecondaryPicture)
                .Select(p => p.PictureId)
                .FirstOrDefault();

            // TODO: See if there is a better way to do this.  I don't think the repository should call methods on the domain entity
            property.CalculateScore();
            
            return property;
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
    }
}