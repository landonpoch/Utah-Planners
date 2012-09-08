using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.Contract.Repository
{
    public interface IPictureRepository
    {
        Picture GetPicture(int pictureId);
        List<Picture> GetPictures(int propertyId);
    }
}
