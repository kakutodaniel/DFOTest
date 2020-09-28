using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using DFO.Test.Application.Contracts;

namespace DFO.Test.Application
{
    public class BaseResponse<T> where T : IBaseContract
    {

        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; private set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; private set; } = new List<string>();

        [JsonProperty("items")]
        public T[] Items { get; private set; }

        [JsonProperty("success")]
        public bool Success { get; private set; }

        public void AddErrors(string error, HttpStatusCode statusCode )
        {
            this.Errors.Add(error);
            this.Success = false;
            this.HttpStatusCode = statusCode;
        }

        public void AddItems(T[] items)
        {
            this.Items = items;
            this.Success = true;
        }
    }
}
