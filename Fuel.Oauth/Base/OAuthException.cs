using System;

namespace Fuel.Oauth.Base
{
    public class OAuthException: Exception
    {
        public int Code { get; set; }

        public OAuthException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
