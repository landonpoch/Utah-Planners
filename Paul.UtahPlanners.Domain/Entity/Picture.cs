//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UtahPlanners.Domain.Entity
{
    [DataContract(IsReference = true)]
    public partial class Picture
    {
    	[DataMember]
        public int id { get; set; }
    	[DataMember]
        public Nullable<short> frontPage { get; set; }
    	[DataMember]
        public Nullable<short> mainPicture { get; set; }
    	[DataMember]
        public Nullable<short> secondaryPicture { get; set; }
    	[DataMember]
        public byte[] binaryData { get; set; }
    	[DataMember]
        public string mimeType { get; set; }
    	[DataMember]
        public Nullable<int> property_id { get; set; }
    
    	[DataMember]
        public virtual Property Property { get; set; }
    }
    
}
