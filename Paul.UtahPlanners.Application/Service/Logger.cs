using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paul.UtahPlanners.Application.Contract;

namespace Paul.UtahPlanners.Application.Service
{
    public class Logger : ILogger
    {
        // TODO: Implement Logging (maybe to twitter or something cool like that)
        #region ILogger Members

        public void Trace(string message)
        {
        }

        public void Trace(string message, Exception innerException)
        {
        }

        public void Warning(string message)
        {
        }

        public void Warning(string message, Exception innerException)
        {
        }

        public void Error(string message)
        {
        }

        public void Error(string message, Exception innerException)
        {            
        }

        #endregion
    }
}