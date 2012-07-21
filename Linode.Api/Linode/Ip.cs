using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Linode
{
    public class Ip
    {
        [JsonProperty("IPADDRESSID")]
        public int Id { get; set; }

        [JsonProperty("ISPUBLIC")]
        public int IsPublicRaw { get; set; }
        public bool IsPublic { get { return IsPublicRaw == 1; } }

        [JsonProperty("ISADDRESS")]
        public String IpAddress { get; set; }

        [JsonProperty("RDNS_NAME")]
        public String RdnsName { get; set; }
        
        [JsonProperty("LINODEID")]
        public int LinodeId { get; set; }
    }
}
