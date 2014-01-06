using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtahPlanners.Domain.Entity
{
    public abstract partial class Aggregate
    {
        public Guid Id { get; private set; }
    }
}
