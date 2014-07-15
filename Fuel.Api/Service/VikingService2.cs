using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fuel.Api.Classes;
using Fuel.Api.Common;
using Fuel.Oauth.Base;
using Fuel.Oauth.Utils;
using Newtonsoft.Json;

namespace Fuel.Api.Service
{
    class VikingService2: IVikingApi
    {
        public async Task<OAuthToken> Login(string username, string password)
        {
            OAuthToken token;
            using (var xauth = new XAuth())
            {
                token = await xauth.XAuthAccessTokenRequest(username, password, Credentials.GetInstance(), "https://mobilevikings.com:443/api/2.0/oauth/access_token/");
            }
            return token;
        }

        public Usage GetUsage()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Msisdn>> GetMsisdnList(OAuthToken token)
        {
            string response;
            using (var oauth = new OAuth())
            {
                var parameters = new List<QueryParameter> { new QueryParameter("alias", "1") };
                response = await oauth.OAuthRequest(Credentials.GetInstance(), token, "https://mobilevikings.com:443/api/2.0/oauth/msisdn_list.json", parameters);
            }
            //TODO: finish this
            if(response != null && !string.IsNullOrWhiteSpace(response))
                return JsonConvert.DeserializeObject<List<Msisdn>>(response);
            return new List<Msisdn>();
        }

        public PricePlan GetPricePlanDetails()
        {
            throw new NotImplementedException();
        }

        public Balance GetSimBalance(string msisdn)
        {
            throw new NotImplementedException();
        }

        public List<Topup> GetTopupHistory(string msisdn)
        {
            throw new NotImplementedException();
        }

        public MsisdnDetails GetSimCardInformation(string msisdn)
        {
            throw new NotImplementedException();
        }

        public VikingPointStatistics GetVikingPointStatistics()
        {
            throw new NotImplementedException();
        }

        public VikingPointLink GetVikingPointLinks()
        {
            throw new NotImplementedException();
        }

        public List<VikingPointReferral> GetVikingPointReferrals()
        {
            throw new NotImplementedException();
        }
    }
}
