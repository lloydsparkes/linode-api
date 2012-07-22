using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Utility
{
    public class NodeResponse
    {
        [JsonProperty("NodeID")]
        public int NodeId { get; set; }
    }
}
