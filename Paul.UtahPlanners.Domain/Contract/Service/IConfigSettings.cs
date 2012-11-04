using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.Contract.Service
{
    public interface IConfigSettings
    {
        Weight Weights { get; set; }
        void ReloadWeights();

        LookupValues LookupValues { get; }
        void ReloadLookupValues();
    }
}
