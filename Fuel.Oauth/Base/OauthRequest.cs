using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Fuel.Oauth.Base
{
    public class OauthRequest: OAuthBase, IDisposable
    {
        private IntPtr _nativeResource = Marshal.AllocHGlobal(100);

        protected async Task<string> OAuthResponseGet(string queryString, string signature, string url)
        {
            try
            {
                queryString += "&oauth_signature=" + WebUtility.UrlEncode(signature);

                if (queryString.Length > 0)
                    url += "?";

                var webRequest = WebRequest.Create(url + queryString) as HttpWebRequest;
                if (webRequest == null)
                    throw new Exception("Can't create webrequest");

                webRequest.Method = "GET";
                var response = await webRequest.GetResponseAsync();
                string responseData;
                using (var responseReader = new StreamReader(response.GetResponseStream()))
                {
                    responseData = responseReader.ReadToEnd();
                }
                if (responseData.Length > 0)
                {
                    return responseData;
                }
            }
            catch (Exception)
            {
                //TODO: handle this exception well by throwing something that is correct
                return string.Empty;
            }
            return string.Empty;
        }

        #region IDisposable
        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~OauthRequest()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            // free native resources if there are any.
            if (_nativeResource == IntPtr.Zero)
                return;
            Marshal.FreeHGlobal(_nativeResource);
            _nativeResource = IntPtr.Zero;
        }
        #endregion
    }
}
