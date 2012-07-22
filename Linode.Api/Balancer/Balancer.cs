using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Balancer
{
    public class Balancer
    {
        [JsonProperty("NODEBALANCERID")]
        public int Id { get; set; }

        [JsonProperty("LABEL")]
        public String Name { get; set; }

        [JsonProperty("HOSTNAME")]
        public String HostName { get; set; }

        [JsonProperty("ADDRESS4")]
        public String IpAddress4 { get; set; }

        [JsonProperty("ADDRESS6")]
        public String IpAddress6 { get; set; }

        [JsonProperty("CLIENTCONNTHROTTLE")]
        public int ClientConnectThrottle { get; set; }

        [JsonProperty("STATUS")]
        public String Status { get; set; }
    }
}
