using Newtonsoft.Json;

namespace Fuel.Api.Classes
{
    public class Msisdn
    {
        [JsonProperty(PropertyName = "msisdn")]
        public string MsIsdn { get; set; }
        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }
    }
}
