using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.POC.Domain
{
    public class Setting<T> : Aggregate
    {
        public Setting(string key, T value, bool eagerLoad = false)
        {
            Key = key;
            Value = value;
            EagerLoad = eagerLoad;
        }

        public string Key { get; private set; }

        public T Value { get; private set; }

        public bool EagerLoad { get; private set; }
    }
}