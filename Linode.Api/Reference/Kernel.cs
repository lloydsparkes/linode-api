using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Reference
{
    public class Kernel
    {
        [JsonProperty("KERNELID")]
        public int Id { get; set; }

        [JsonProperty("LABEL")]
        public String Name { get; set; }

        [JsonProperty("ISXEN")]
        public int IsXenRaw { get; set; }
        public bool IsXen { get { return IsXenRaw == 1; } }

        [JsonProperty("ISPVOPS")]
        public int IsPVOPSRaw { get; set; }
        public bool IsPVOPS { get { return IsPVOPSRaw == 1; } }
    }
}
