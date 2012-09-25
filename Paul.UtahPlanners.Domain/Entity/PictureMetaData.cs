using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UtahPlanners.Domain.Entity
{
    [DataContract(IsReference = true)]
    public class PictureMetaData
    {
        [DataMember]
        public int PictureId { get; set; }
        [DataMember]
        public bool PrimaryPicture { get; set; }
        [DataMember]
        public bool SecondaryPicture { get; set; }
        [DataMember]
        public bool FrontPage { get; set; }
        [DataMember]
        public bool Delete { get; set; }
    }
}
