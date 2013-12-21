using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.POC.Domain
{
    public abstract partial class Aggregate
    {
        public Guid Id { get; private set; }
    }

    //// Extensions
    //public abstract partial class Aggregate
    //{
    //    public Guid SqlId { get; private set; }
    //    public ObjectId MongoId { get; private set; }
    //}
}