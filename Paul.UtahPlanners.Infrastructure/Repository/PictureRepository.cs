using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain;
using System.Data.Entity;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Domain.Contract.Repository;

namespace UtahPlanners.Infrastructure.Repository
{
    public class PictureRepository : IPictureRepository
    {
        private DbSet<Picture> _pics;

        public PictureRepository(DbSet<Picture> pics)
        {
            _pics = pics;
        }

        #region IPictureRepository Members

        public Picture GetPicture(int pictureId)
        {
            return _pics.FirstOrDefault(p => p.id == pictureId);
        }

        public List<Picture> GetPictures(int propertyId)
        {
            return _pics.Where(p => p.property_id == propertyId)
                .ToList();
        }

        #endregion
    }
}
