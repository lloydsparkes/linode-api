using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Utility
{
    public class ConfigResponse
    {
        [JsonProperty("ConfigID")]
        public int ConfigId { get; set; }
    }
}
