using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fuel.Oauth.Base;

namespace Fuel.Oauth.Utils
{
    public class XAuth: OauthRequest
    {
        public async Task<OauthToken> XAuthAccessTokenRequest(string username, string password, ICredential credentials, string xauthUri)
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
                        return new OauthToken {TokenKey = qs["oauth_token"], TokenSecret = qs["oauth_token_secret"]};
                    }
                }
            }
            catch (Exception)
            {
                //TODO: handle this exception well by throwing something that is correct
                return new OauthToken();
            }
            return new OauthToken();
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
