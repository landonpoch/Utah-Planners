using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Entity;

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
