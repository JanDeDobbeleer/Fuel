namespace Fuel.Oauth.Base
{
    public class QueryParameter
    {
        private readonly string _name;
        private readonly string _value;

        public QueryParameter(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Value
        {
            get { return _value; }
        }
    }
}
