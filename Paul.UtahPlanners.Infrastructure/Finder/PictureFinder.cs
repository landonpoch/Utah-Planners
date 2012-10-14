using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Domain.Contract.Finder;

namespace UtahPlanners.Infrastructure.Finder
{
    public class PictureFinder : IPictureFinder
    {
        private DbSet<Picture> _pics;

        public PictureFinder(DbSet<Picture> pics)
        {
            _pics = pics;
        }

        #region IPictureRepository Members

        public Picture FindPicture(int pictureId)
        {
            return _pics.FirstOrDefault(p => p.id == pictureId);
        }

        #endregion
    }
}
