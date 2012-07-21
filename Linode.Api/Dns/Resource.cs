using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.ObjectModel.Dns
{
    public enum ResourceTypeEnum
    {
        NullRecord = 0,
        NS,
        MX,
        A,
        AAAA,
        CNAME,
        TXT,
        SRV
    }

    public class Resource
    {
        [JsonProperty("RESOURCEID")]
        public int Id { get; set; }

        [JsonProperty("PROTOCOL")]
        public String Protocol { get; set; }

        [JsonProperty("PRIORITY")]
        public int Priority { get; set; }

        [JsonProperty("TYPE")]
        public String TypeRaw { get; set; }
        public ResourceTypeEnum Type
        {
            get
            {
                if (string.IsNullOrEmpty(TypeRaw))
                    return ResourceTypeEnum.NullRecord;

                try
                {
                    return (ResourceTypeEnum)Enum.Parse(typeof(ResourceTypeEnum), TypeRaw, true);
                }
                catch (Exception e)
                {
                    return ResourceTypeEnum.NullRecord;
                }
            }
        }

        [JsonProperty("TARGET")]
        public String TargetIpAddress { get; set; }

        [JsonProperty("WEIGHT")]
        public int Weight { get; set; }

        [JsonProperty("PORT")]
        public String Port { get; set; }

        [JsonProperty("NAME")]
        public String Name { get; set; }
    }
}
