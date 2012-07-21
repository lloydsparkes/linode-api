using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Base
{
    public class Response<T>
    {
        [JsonProperty("ERRORARRAY")]
        public Error[] Errors { get; set; }

        [JsonProperty("ACTION")]
        public string Action { get; set; }

        [JsonProperty("DATA")]
        public T Data { get; set; }
    }
}
