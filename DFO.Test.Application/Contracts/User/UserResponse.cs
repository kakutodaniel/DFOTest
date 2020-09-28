using Newtonsoft.Json;
using System;

namespace DFO.Test.Application.Contracts.User
{
    public class UserResponse : IBaseContract
    {
        public UserResponse() { }
        
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("hashId")]
        public string HashId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("updateDate")]
        public DateTime?  UpdateDate { get; set; }


    }
}
