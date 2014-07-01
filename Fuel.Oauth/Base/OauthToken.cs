using System;

namespace Fuel.Oauth.Base
{
    public class OAuthToken
    {
        public string TokenKey { get; set; }
        public string TokenSecret { get; set; }

        public OAuthToken()
        {
            TokenKey = String.Empty;
            TokenSecret = String.Empty;
        }
    }
}
