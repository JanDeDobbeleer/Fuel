using System;

namespace Fuel.Oauth.Base
{
    public class OauthToken
    {
        public string TokenKey { get; set; }
        public string TokenSecret { get; set; }

        public OauthToken()
        {
            TokenKey = String.Empty;
            TokenSecret = String.Empty;
        }
    }
}
