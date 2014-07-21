using Fuel.Oauth.Base;

namespace Fuel.Test.Oauth
{
    public class Credentials: ICredential
    {
        #region Constructor
        private static Credentials _sInstance;
        private static readonly object SInstanceSync = new object();

        protected Credentials()
        {
        }

        /// <summary>
        /// Returns an instance (a singleton)
        /// </summary>
        /// <returns>a singleton</returns>
        /// <remarks>
        /// This is an implementation of the singelton design pattern.
        /// </remarks>
        public static Credentials GetInstance()
        {
            // This implementation of the singleton design pattern prevents 
            // unnecessary locks (using the double if-test)
            if (_sInstance == null)
            {
                lock (SInstanceSync)
                {
                    if (_sInstance == null)
                    {
                        _sInstance = new Credentials();
                    }
                }
            }
            return _sInstance;
        }
        #endregion


        public string ConsumerKey { get { return "un9HyMLftXRtDf89jP"; } }
        public string ConsumerSecret { get { return "AaDM9yyXLmTemvM2nVahzBFYS9JG62a6"; } }
    }
}
