using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.Contract.Finder
{
    public interface IPictureFinder
    {
        Picture FindPicture(int pictureId);
    }
}
