using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Utility
{
    public class BalancerResponse
    {
        [JsonProperty("NodeBalancerID")]
        public int NodeBalancerId { get; set; }
    }
}
