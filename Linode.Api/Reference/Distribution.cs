using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Reference
{
    public class Distribution
    {
        [JsonProperty("DISTRIBUTIONID")]
        public int Id { get; set; }

        [JsonProperty("LABEL")]
        public String Name { get; set; }

        [JsonProperty("IS64BIT")]
        public int Is64BitRaw { get; set; }
        public bool Is64Bit { get { return Is64BitRaw == 1; } }

        [JsonProperty("MINIMAGESIZE")]
        public int MinimumImageSize { get; set; }

        [JsonProperty("CREATE_DT")]
        public DateTime Created { get; set; }

        [JsonProperty("REQUIRESPVOPSKERNEL")]
        public int RequiresPVOPSKernelRaw { get; set; }
        public bool RequiresPVOPSKernel { get { return RequiresPVOPSKernelRaw == 1; } }
    }
}
