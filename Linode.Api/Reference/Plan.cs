using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Reference
{
    public class Plan
    {
        [JsonProperty("PLANID")]
        public int Id { get; set; }

        [JsonProperty("LABEL")]
        public String Name { get; set; }

        [JsonProperty("DISK")]
        public int DiskSpace { get; set; }

        [JsonProperty("PRICE")]
        public double Price { get; set; }

        [JsonProperty("RAM")]
        public int Memory { get; set; }

        [JsonProperty("XFER")]
        public int Xfer { get; set; }

        //Availability
        [JsonProperty("AVAIL")]
        public Dictionary<int,int> Availability { get; set; }
    }
}
