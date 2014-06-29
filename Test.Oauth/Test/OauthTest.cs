using System.Collections.Generic;
using System.Threading.Tasks;
using Fuel.Oauth.Base;
using Fuel.Oauth.Utils;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Test.Oauth.Test
{
    [TestClass]
    public class OauthTest
    {
        [TestMethod]
        public async Task TestLogin()
        {
            OauthToken token;
            using (var xauth = new XAuth())
            {
                token = await xauth.XAuthAccessTokenRequest("test", "test", Credentials.Credentials.GetInstance(), "https://mobilevikings.com:443/api/2.0/oauth/access_token/");
            }
            Assert.AreNotEqual(string.Empty, token.TokenKey);
            Assert.AreNotEqual(string.Empty, token.TokenSecret);
        }

        [TestMethod]
        public async Task TestGetSimDetails()
        {
            OauthToken token;
            using (var xauth = new XAuth())
            {
                token = await xauth.XAuthAccessTokenRequest("test", "test", Credentials.Credentials.GetInstance(), "https://mobilevikings.com:443/api/2.0/oauth/access_token/");
            }
            string response;
            using (var oauth = new Fuel.Oauth.Utils.Oauth())
            {
                var parameters = new List<QueryParameter> { new QueryParameter("alias", "1") };
                response = await oauth.OAuthRequest(Credentials.Credentials.GetInstance(), token, "https://mobilevikings.com:443/api/2.0/oauth/msisdn_list.json", parameters);
            }
            Assert.AreNotEqual(string.Empty, response);
        }
    }
}
