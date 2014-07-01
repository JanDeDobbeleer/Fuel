using System;
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
            OAuthToken token;
            using (var xauth = new XAuth())
            {
                token = await xauth.XAuthAccessTokenRequest("test", "test", Credentials.GetInstance(), "https://mobilevikings.com:443/api/2.0/oauth/access_token/");
            }
            Assert.AreNotEqual(string.Empty, token.TokenKey);
            Assert.AreNotEqual(string.Empty, token.TokenSecret);
        }

        [TestMethod]
        public async Task TestGetSimDetails()
        {
            OAuthToken token;
            using (var xauth = new XAuth())
            {
                token = await xauth.XAuthAccessTokenRequest("test", "test", Credentials.GetInstance(), "https://mobilevikings.com:443/api/2.0/oauth/access_token/");
            }
            string response;
            using (var oauth = new OAuth())
            {
                var parameters = new List<QueryParameter> { new QueryParameter("alias", "1") };
                response = await oauth.OAuthRequest(Credentials.GetInstance(), token, "https://mobilevikings.com:443/api/2.0/oauth/msisdn_list.json", parameters);
            }
            Assert.AreNotEqual(string.Empty, response);
        }

        [TestMethod]
        public async Task TestExceptionInvalidPassword()
        {
            try
            {
                OAuthToken token;
                using (var xauth = new XAuth())
                {
                    token =
                        await
                            xauth.XAuthAccessTokenRequest("test", "noneshallpass", Credentials.GetInstance(),
                                "https://mobilevikings.com:443/api/2.0/oauth/access_token/");
                }
                Assert.Fail();
            }
            catch (OAuthException e)
            {
                //Seriously though, a 500 upon providing a wrong password?!
                Assert.AreEqual(e.Code, 500);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task TestExceptionInvalidEndpoint()
        {
            try
            {
                OAuthToken token;
                using (var xauth = new XAuth())
                {
                    token =
                        await
                            xauth.XAuthAccessTokenRequest("test", "test", Credentials.GetInstance(),
                                "https://mobilevikings.com:443/api/2.0/oauth/access_/");
                }
                Assert.Fail();
            }
            catch (OAuthException e)
            {
                Assert.AreEqual(e.Code, 404);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
