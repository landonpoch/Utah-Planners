using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace UtahPlanners.MVC3.Helper
{
    public static class WcfExtensions
    {
        #region SafeExecution
        public static TResult SafeExecution<TExtension, TResult>
            (this TExtension myServiceClient, Func<TExtension, TResult> serviceAction)
            where TExtension : ICommunicationObject
        {
            TResult outValue;
            try
            {
                outValue = serviceAction.Invoke(myServiceClient);
            }
            finally
            {
                myServiceClient.CloseConnection();
            }
            return outValue;
        }

        /// <summary>
        /// Safely closes a service client connection.
        /// </summary>
        /// <param name="myServiceClient">The client connection to close.</param>
        public static void CloseConnection(this ICommunicationObject myServiceClient)
        {
            if (myServiceClient.State != CommunicationState.Opened)
            {
                return;
            }

            try
            {
                myServiceClient.Close();
            }
            catch (CommunicationException)
            {
                myServiceClient.Abort();
            }
            catch (TimeoutException)
            {
                myServiceClient.Abort();
            }
            catch (Exception)
            {
                myServiceClient.Abort();
                throw;
            }
        }
        #endregion

        #region Service Use
        public delegate void UseServiceDelegate<T>(T proxy);

        public static class Service<T>
        {
            public static ChannelFactory<T> _channelFactory = new ChannelFactory<T>("");

            public static void Use(UseServiceDelegate<T> codeBlock)
            {
                IClientChannel proxy = (IClientChannel)_channelFactory.CreateChannel();
                bool success = false;
                try
                {
                    codeBlock((T)proxy);
                    proxy.Close(); 
                    success = true;
                }
                finally
                {
                    if (!success)
                    {
                        proxy.Abort();
                    }
                }
            }
        }
        #endregion
    }

    #region Using Wrapper
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
    #endregion
}