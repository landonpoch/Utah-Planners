using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace UtahPlanners.MVC3.Presentation
{
    public class WcfClient<T> : IDisposable
        where T : ICommunicationObject
    {
        public T Client { get; set; }

        public WcfClient(T client)
        {
            Client = client;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (Client.State != CommunicationState.Opened)
            {
                return;
            }

            try
            {
                Client.Close();
            }
            catch (CommunicationException)
            {
                Client.Abort();
            }
            catch (TimeoutException)
            {
                Client.Abort();
            }
            catch (Exception)
            {
                Client.Abort();
                throw;
            }
        }

        #endregion
    }
}