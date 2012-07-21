using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Reference
{
    public class DataCenter
    {
        [JsonProperty("DATACENTERID")]
        public int Id { get; set; }

        [JsonProperty("LOCATION")]
        public String Location { get; set; }
    }
}
