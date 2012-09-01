using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain
{
    public interface IConfigSettings
    {
        Weight Weights { get; }
        void ReloadWeights();
    }
}
