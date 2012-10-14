using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paul.UtahPlanners.Application.Contract
{
    public interface ILogger
    {
        void Trace(string message);
        void Trace(string message, Exception innerException);
        void Warning(string message);
        void Warning(string message, Exception innerException);
        void Error(string message);
        void Error(string message, Exception innerException);
    }
}
