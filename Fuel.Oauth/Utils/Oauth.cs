using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fuel.Oauth.Base;

namespace Fuel.Oauth.Utils
{
    public class Oauth : OauthRequest
    {
        public async Task<string> OAuthRequest(ICredential credentials, OauthToken oauthToken, string oauthUri, IList<QueryParameter> parameters)
        {
            var uri = oauthUri + "?" + NormalizeRequestParameters(parameters);
            return await OAuthRequest(credentials, oauthToken, uri);
        }

        private async Task<string> OAuthRequest(ICredential credentials, OauthToken oauthToken, string oauthUri)
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
                    oauthToken.TokenKey,
                    oauthToken.TokenSecret,
                    "GET",
                    timeStamp,
                    nonce,
                    out outUrl,
                    out querystring);

                return await OAuthResponseGet(querystring, sig, outUrl);
            }
            catch (Exception)
            {
                //TODO: handle this exception well by throwing something that is correct
                return string.Empty;
            }
        }
    }
}
