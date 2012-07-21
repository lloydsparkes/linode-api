using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Balancer
{
    public enum ModeEnum
    {
        Accept,
        Reject,
        Drain,
        NullRecord
    }

    public class BalancerNode
    {
        [JsonProperty("NODEID")]
        public int NodeId { get; set; }

        [JsonProperty("CONFIGID")]
        public int ConfigId { get; set; }

        [JsonProperty("NODEBALANCERID")]
        public int BalancerId { get; set; }

        [JsonProperty("LABEL")]
        public String Name { get; set; }

        [JsonProperty("WEIGHT")]
        public int Weight { get; set; }

        [JsonProperty("MODE")]
        public string ModeRaw { get; set; }
        public ModeEnum Mode
        {
            get
            {
                if (string.IsNullOrEmpty(ModeRaw))
                    return ModeEnum.NullRecord;

                try
                {
                    return (ModeEnum)Enum.Parse(typeof(ModeEnum), ModeRaw, true);
                }
                catch (Exception e)
                {
                    return ModeEnum.NullRecord;
                }
            }
        }

        [JsonProperty("Status")]
        public String Status { get; set; }

        [JsonProperty("Address")]
        public String IpAddress { get; set; }
    }
}
