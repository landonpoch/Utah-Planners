using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace UtahPlanners.MVC3.Extensions
{
    public static class WcfExtensions
    {
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
    }
}