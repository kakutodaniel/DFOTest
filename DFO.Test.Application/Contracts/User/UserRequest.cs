using Newtonsoft.Json;

namespace DFO.Test.Application.Contracts.User
{
    public class UserRequest
    {
        public UserRequest() { }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

    }
}
