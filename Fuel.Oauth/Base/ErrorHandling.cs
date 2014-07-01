namespace Fuel.Oauth.Base
{
    static class ErrorHandling
    {
        public static OAuthException GetOauthErrorMessage(this string T)
        {
            var lowerCase = T.ToLower();
            if (lowerCase.Contains("400") && lowerCase.Contains("invalid consumer"))
                return new OAuthException(400, "invalid consumer");
            if (lowerCase.Contains("400") && lowerCase.Contains("invalid request token"))
                return new OAuthException(400, "request token expired");
            if (lowerCase.Contains("400") && lowerCase.Contains("could not verify"))
                return new OAuthException(400, "access denied");
            if (lowerCase.Contains("400") && lowerCase.Contains("invalid oauth verifier"))
                return new OAuthException(400, "invalid verifier");
            if (lowerCase.Contains("400") && lowerCase.Contains("missing oauth parameters"))
                return new OAuthException(400, "missing parameters");
            if (lowerCase.Contains("403") && lowerCase.Contains("xauth not allowed for this consumer"))
                return new OAuthException(403, "xAuth not allowed");
            if (lowerCase.Contains("403") && lowerCase.Contains("xauth username/password combination invalid"))
                return new OAuthException(403, "wrong username/password combination");
            if (lowerCase.Contains("404"))
                return new OAuthException(404, "invalid method");
            return lowerCase.Contains("401")
                ? new OAuthException(401, "not authorized")
                : new OAuthException(500, "server error");
        }
    }
}
