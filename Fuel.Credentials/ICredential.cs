namespace Fuel.Credentials
{
    interface ICredential
    {
        string ConsumerKey { get; }
        string ConsumerSecret { get; }
    }
}
