using System.Collections.Generic;
using System.Threading.Tasks;
using Fuel.Api.Classes;
using Fuel.Api.Common;
using Fuel.Oauth.Base;

namespace Fuel.Api.Service
{
    public class VikingServiceFactory: IVikingApi
    {
        private readonly IVikingApi _api;

        #region Constructor
        private static VikingServiceFactory _sInstance;
        private static readonly object SInstanceSync = new object();

        protected VikingServiceFactory(IVikingApi api)
        {
            _api = api;
        }

        /// <summary>
        /// Returns an instance (a singleton)
        /// </summary>
        /// <returns>a singleton</returns>
        /// <remarks>
        /// This is an implementation of the singelton design pattern.
        /// </remarks>
        public static VikingServiceFactory GetInstance()
        {
            // This implementation of the singleton design pattern prevents 
            // unnecessary locks (using the double if-test)
            if (_sInstance == null)
            {
                lock (SInstanceSync)
                {
                    if (_sInstance == null)
                    {
                        _sInstance = new VikingServiceFactory(new VikingService2());
                    }
                }
            }
            return _sInstance;
        }
        #endregion

        public Task<OAuthToken> Login(string username, string password)
        {
            return _api.Login(username, password);
        }

        public Usage GetUsage()
        {
            return _api.GetUsage();
        }

        public Task<List<Msisdn>> GetMsisdnList(OAuthToken token)
        {
            return _api.GetMsisdnList(token);
        }

        public PricePlan GetPricePlanDetails()
        {
            return _api.GetPricePlanDetails();
        }

        public Task<Balance> GetSimBalance(string msisdn, OAuthToken token)
        {
            return _api.GetSimBalance(msisdn, token);
        }

        public List<Topup> GetTopupHistory(string msisdn)
        {
            return _api.GetTopupHistory(msisdn);
        }

        public MsisdnDetails GetSimCardInformation(string msisdn)
        {
            return _api.GetSimCardInformation(msisdn);
        }

        public VikingPointStatistics GetVikingPointStatistics()
        {
            return _api.GetVikingPointStatistics();
        }

        public VikingPointLink GetVikingPointLinks()
        {
            return _api.GetVikingPointLinks();
        }

        public List<VikingPointReferral> GetVikingPointReferrals()
        {
            return _api.GetVikingPointReferrals();
        }
    }
}
