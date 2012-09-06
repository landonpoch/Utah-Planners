using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain
{
    public interface IConfigRepository
    {
        Weight GetWeights();
        LookupValues GetLookupValues();
    }
}
