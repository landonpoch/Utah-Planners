using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain
{
    public interface IPictureRepository
    {
        Picture GetPicture(int pictureId);
        List<Picture> GetPictures(int propertyId);
    }
}
