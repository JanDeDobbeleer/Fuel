namespace Fuel.Oauth.Base
{
    public interface ICredential
    {
        string ConsumerKey { get; }
        string ConsumerSecret { get; }
    }
}
