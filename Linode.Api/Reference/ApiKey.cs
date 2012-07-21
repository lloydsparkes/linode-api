using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Reference
{
    public class ApiKey
    {
        [JsonProperty("USERNAME")]
        public String Username { get; set; }

        [JsonProperty("API_KEY")]
        public String Key { get; set; }
    }
}
