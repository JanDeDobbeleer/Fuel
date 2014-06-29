using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace Fuel.Oauth.Base
{
    public enum SignatureTypes
    {
        HMACSHA1,
        PLAINTEXT,
        RSASHA1
    }

    public class OAuthBase
    {
        protected string UnreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        protected const string OAuthVersion = "1.0";
        protected const string OAuthParameterPrefix = "oauth_";

        //
        // List of known and used oauth parameters' names
        //        
        protected const string OAuthConsumerKeyKey = "oauth_consumer_key";
        protected const string OAuthCallbackKey = "oauth_callback";
        protected const string OAuthVersionKey = "oauth_version";
        protected const string OAuthSignatureMethodKey = "oauth_signature_method";
        protected const string OAuthSignatureKey = "oauth_signature";
        protected const string OAuthTimestampKey = "oauth_timestamp";
        protected const string OAuthNonceKey = "oauth_nonce";
        protected const string OAuthTokenKey = "oauth_token";
        protected const string OAuthTokenSecretKey = "oauth_token_secret";
        protected const string OAuthVerifier = "oauth_verifier";
        protected const string XAuthUsername = "x_auth_username";
        protected const string XAuthPassword = "x_auth_password";
        protected const string XAuthMode = "x_auth_mode";


        protected const string Hmacsha1SignatureType = "HMAC-SHA1";
        protected const string PlainTextSignatureType = "PLAINTEXT";
        protected const string Rsasha1SignatureType = "RSA-SHA1";

        protected Random Random = new Random();


        private string ComputeHash(string data, string keyString)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            var crypt = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
            var keyBuffer = CryptographicBuffer.ConvertStringToBinary(keyString, BinaryStringEncoding.Utf8);
            var key = crypt.CreateKey(keyBuffer);
            var dataBuffer = CryptographicBuffer.CreateFromByteArray(Encoding.UTF8.GetBytes(data));
            var sigBuffer = CryptographicEngine.Sign(key, dataBuffer);
            return CryptographicBuffer.EncodeToBase64String(sigBuffer);
        }

        protected List<QueryParameter> GetQueryParameters(string parameters)
        {
            if (parameters.StartsWith("?"))
            {
                parameters = parameters.Remove(0, 1);
            }

            var result = new List<QueryParameter>();

            if (string.IsNullOrEmpty(parameters))
                return result;
            var p = parameters.Split('&');
            foreach (var s in p)
            {
                if (string.IsNullOrEmpty(s) || s.StartsWith(OAuthParameterPrefix))
                    continue;
                if (s.IndexOf('=') > -1)
                {
                    var temp = s.Split('=');
                    result.Add(new QueryParameter(temp[0], temp[1]));
                }
                else
                {
                    result.Add(new QueryParameter(s, string.Empty));
                }
            }

            return result;
        }

        public string UrlEncode(string value)
        {
            var result = new StringBuilder();

            foreach (var symbol in value)
            {
                if (UnreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    //some symbols produce > 2 char values so the system urlencoder must be used to get the correct data
                    if (String.Format("{0:X2}", (int)symbol).Length > 3)
                    {
                        result.Append(WebUtility.UrlEncode(value.Substring(value.IndexOf(symbol), 1)).ToUpper());
                    }
                    else
                    {
                        result.Append('%' + String.Format("{0:X2}", (int)symbol));
                    }
                }
            }
            return result.ToString();
        }

        protected string NormalizeRequestParameters(IList<QueryParameter> parameters)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < parameters.Count; i++)
            {
                var p = parameters[i];
                sb.AppendFormat("{0}={1}", p.Name, p.Value);

                if (i < parameters.Count - 1)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }

        public string GenerateSignatureBase(Uri url, string consumerKey, string token, string verifier, string xAuthUsername, string xAuthPassword, string httpMethod, string timeStamp, string nonce, string signatureType, out string normalizedUrl, out string normalizedRequestParameters)
        {
            normalizedUrl = null;
            normalizedRequestParameters = null;

            if (token == null)
            {
                token = string.Empty;
            }

            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            if (string.IsNullOrEmpty(httpMethod))
            {
                throw new ArgumentNullException("httpMethod");
            }

            if (string.IsNullOrEmpty(signatureType))
            {
                throw new ArgumentNullException("signatureType");
            }

            var parameters = GetQueryParameters(url.Query);
            parameters.Add(new QueryParameter(OAuthVersionKey, OAuthVersion));
            parameters.Add(new QueryParameter(OAuthNonceKey, nonce));
            parameters.Add(new QueryParameter(OAuthTimestampKey, timeStamp));
            parameters.Add(new QueryParameter(OAuthSignatureMethodKey, signatureType));
            parameters.Add(new QueryParameter(OAuthConsumerKeyKey, consumerKey));

            if (!string.IsNullOrEmpty(token))
            {
                parameters.Add(new QueryParameter(OAuthTokenKey, token));
            }

            if (!string.IsNullOrEmpty(verifier))
            {
                parameters.Add(new QueryParameter(OAuthVerifier, verifier));
            }

            if (!string.IsNullOrEmpty(xAuthUsername))
            {
                parameters.Add(new QueryParameter(XAuthUsername, UrlEncode(xAuthUsername)));
            }

            if (!string.IsNullOrEmpty(xAuthPassword))
            {
                parameters.Add(new QueryParameter(XAuthPassword, UrlEncode(xAuthPassword)));
                parameters.Add(new QueryParameter(XAuthMode, "client_auth"));

            }

            parameters.Sort(new QueryParameterComparer());

            normalizedUrl = string.Format("{0}://{1}", url.Scheme, url.Host);
            if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443)))
            {
                normalizedUrl += ":" + url.Port;
            }
            normalizedUrl += url.AbsolutePath;
            normalizedRequestParameters = NormalizeRequestParameters(parameters);

            var signatureBase = new StringBuilder();
            signatureBase.AppendFormat("{0}&", httpMethod.ToUpper());
            signatureBase.AppendFormat("{0}&", UrlEncode(normalizedUrl));
            signatureBase.AppendFormat("{0}", UrlEncode(normalizedRequestParameters));

            return signatureBase.ToString();
        }

        public string GenerateSignatureUsingHash(string signatureBase, string keyString)
        {
            return ComputeHash(signatureBase, keyString);
        }

        public string GenerateSignature(Uri url, string consumerKey, string consumerSecret, string token, string tokenSecret, string verifier, string xAuthUsername, string xAuthPassword, string httpMethod, string timeStamp, string nonce, out string normalizedUrl, out string normalizedRequestParameters)
        {
            var signatureBase = GenerateSignatureBase(url, consumerKey, token, verifier, xAuthUsername, xAuthPassword, httpMethod, timeStamp, nonce, Hmacsha1SignatureType, out normalizedUrl, out normalizedRequestParameters);
            return GenerateSignature(consumerSecret, tokenSecret, signatureBase);
        }

        public string GenerateSignature(Uri url, string consumerKey, string consumerSecret, string token, string tokenSecret, string httpMethod, string timeStamp, string nonce, out string normalizedUrl, out string normalizedRequestParameters)
        {
            var signatureBase = GenerateSignatureBase(url, consumerKey, token, string.Empty, string.Empty, string.Empty, httpMethod, timeStamp, nonce, Hmacsha1SignatureType, out normalizedUrl, out normalizedRequestParameters);
            return GenerateSignature(consumerSecret, tokenSecret, signatureBase);
        }

        public string GenerateSignature(string consumerSecret, string tokenSecret, string signatureBase)
        {
            var keyString = string.Format("{0}&{1}", UrlEncode(consumerSecret), string.IsNullOrEmpty(tokenSecret) ? "" : UrlEncode(tokenSecret));
            return GenerateSignatureUsingHash(signatureBase, keyString);
        }

        public virtual string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public virtual string GenerateNonce()
        {
            // Just a simple implementation of a random number between 123400 and 9999999
            return Random.Next(123400, 9999999).ToString();
        }

    }
}
