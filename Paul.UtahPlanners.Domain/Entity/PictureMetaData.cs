using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain.Entity
{
    public class PictureMetaData
    {
        public int PictureId { get; set; }
        public bool PrimaryPicture { get; set; }
        public bool SecondaryPicture { get; set; }
        public bool FrontPage { get; set; }
        public bool Delete { get; set; }
    }
}
