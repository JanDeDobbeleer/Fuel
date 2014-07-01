using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fuel.Oauth.Base;

namespace Fuel.Oauth.Utils
{
    public class OAuth : OAuthRequest
    {
        public async Task<string> OAuthRequest(ICredential credentials, OAuthToken oAuthToken, string oauthUri, IList<QueryParameter> parameters)
        {
            var uri = oauthUri + "?" + NormalizeRequestParameters(parameters);
            return await OAuthRequest(credentials, oAuthToken, uri);
        }

        private async Task<string> OAuthRequest(ICredential credentials, OAuthToken oAuthToken, string oauthUri)
        {
            try
            {
                string outUrl;
                string querystring;
                var nonce = GenerateNonce();
                var timeStamp = GenerateTimeStamp();
                var sig = GenerateSignature(new Uri(oauthUri),
                    credentials.ConsumerKey,
                    credentials.ConsumerSecret,
                    oAuthToken.TokenKey,
                    oAuthToken.TokenSecret,
                    "GET",
                    timeStamp,
                    nonce,
                    out outUrl,
                    out querystring);

                return await OAuthResponseGet(querystring, sig, outUrl);
            }
            catch (Exception e)
            {
                if (e is OAuthException)
                    throw;
                return string.Empty;
            }
        }
    }
}
