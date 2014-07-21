using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fuel.Api.Classes;
using Fuel.Api.Service;
using Fuel.Oauth.Base;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Fuel.Test.Api
{
    [TestClass]
    public class TestVikingService
    {
        private OAuthToken _token;

        [TestInitialize]
        public async Task MyTestInitialize()
        {
            if(_token == null)
                _token = await VikingServiceFactory.GetInstance().Login("jan.de.dobbeleer@gmail.com", "Sgom1981jj?");
        }

        [TestMethod]
        public async Task TestSimList()
        {
            var msisdn = await VikingServiceFactory.GetInstance().GetMsisdnList(_token);
            Assert.AreNotEqual(new List<Msisdn>(), msisdn);
        }

        [TestMethod]
        public async Task TestBalance()
        {
            var balance = await VikingServiceFactory.GetInstance().GetSimBalance("+32470598580", _token);
            Assert.AreNotEqual(new Balance(), balance);
        }
    }
}
