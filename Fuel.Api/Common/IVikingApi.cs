using System.Collections.Generic;
using System.Threading.Tasks;
using Fuel.Api.Classes;
using Fuel.Oauth.Base;

namespace Fuel.Api.Common
{
    public interface IVikingApi
    {
        Task<OAuthToken> Login(string username, string password);
        Usage GetUsage();
        Task<List<Msisdn>> GetMsisdnList(OAuthToken token);
        PricePlan GetPricePlanDetails();
        Balance GetSimBalance(string msisdn);
        List<Topup> GetTopupHistory(string msisdn);
        MsisdnDetails GetSimCardInformation(string msisdn);
        VikingPointStatistics GetVikingPointStatistics();
        VikingPointLink GetVikingPointLinks();
        List<VikingPointReferral> GetVikingPointReferrals();
    }
}
