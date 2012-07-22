using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Utility
{
    public class DiskResponse
    {
        [JsonProperty("JobID")]
        public int JobId { get; set; }

        [JsonProperty("DiskID")]
        public int DiskId { get; set; }
    }
}
