using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UtahPlanners.Domain.Entity
{
    public class Picture { } // Delete this class in favor of using the good old filesystem

    public class PictureMetaData : Aggregate
    {
        public PictureMetaData(Guid propertyId,
            string path,
            string mimeType,
            bool mainPicture,
            bool secondaryPicture,
            bool showcasePicture)
        {
            PropertyId = propertyId;
            Path = path;
            MimeType = mimeType;
            MainPicture = mainPicture;
            SecondaryPicture = secondaryPicture;
            ShowcasePicture = showcasePicture;
        }

        public Guid PropertyId { get; private set; }

        public string Path { get; private set; }

        public string MimeType { get; private set; }

        public bool MainPicture { get; private set; }

        public bool SecondaryPicture { get; private set; }

        public bool ShowcasePicture { get; private set; }
    }
}
