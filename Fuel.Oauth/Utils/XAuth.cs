using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fuel.Oauth.Base;

namespace Fuel.Oauth.Utils
{
    public class XAuth: OAuthRequest
    {
        public async Task<OAuthToken> XAuthAccessTokenRequest(string username, string password, ICredential credentials, string xauthUri)
        {
            try
            {
                string outUrl;
                string querystring;
                var nonce = GenerateNonce();
                var timeStamp = GenerateTimeStamp();
                var sig = GenerateSignature(new Uri(xauthUri),
                    credentials.ConsumerKey,
                    credentials.ConsumerSecret,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    username,
                    password,
                    "GET",
                    timeStamp,
                    nonce,
                    out outUrl,
                    out querystring);

                var responseData = await OAuthResponseGet(querystring, sig, outUrl);

                if (responseData.Length > 0)
                {
                    var qs = GetQueryParameters(responseData);
                    if (qs["oauth_token"] != null && qs["oauth_token_secret"] != null)
                    {
                        return new OAuthToken {TokenKey = qs["oauth_token"], TokenSecret = qs["oauth_token_secret"]};
                    }
                }
            }
            catch (Exception e)
            {
                if (e is OAuthException)
                    throw;
                return new OAuthToken();
            }
            return new OAuthToken();
        }

        private new Dictionary<string, string> GetQueryParameters(string response)
        {
            var nameValueCollection = new Dictionary<string, string>();
            string[] items = response.Split('&');

            foreach (string item in items)
            {
                if (item.Contains("="))
                {
                    string[] nameValue = item.Split('=');
                    if (nameValue[0].Contains("?"))
                        nameValue[0] = nameValue[0].Replace("?", "");
                    nameValueCollection.Add(nameValue[0], WebUtility.UrlDecode(nameValue[1]));
                }
            }
            return nameValueCollection;
        }
    }
}
