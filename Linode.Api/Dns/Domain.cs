using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Dns
{
    public enum DomainStatusEnum
    { 
        Disabled = 0,
        Active = 1,
        EditMode = 2
    }

    public enum DomainTypeEnum
    {
        Master,
        Slave
    }

    public class Domain
    {
        [JsonProperty("DOMAINID")]
        public int Id { get; set; }

        [JsonProperty("DOMAIN")]
        public String Name { get; set; }

        [JsonProperty("DESCRIPTION")]
        public String Description { get; set; }

        [JsonProperty("TYPE")]
        public String TypeRaw { get; set; }
        public DomainTypeEnum Type
        {
            get
            {
                if (string.IsNullOrEmpty(TypeRaw))
                    return DomainTypeEnum.Master;

                try
                {
                    return (DomainTypeEnum)Enum.Parse(typeof(DomainTypeEnum), TypeRaw, true);
                }
                catch (Exception e)
                {
                    return DomainTypeEnum.Master;
                }
            }
        }

        [JsonProperty("SOA_EMAIL")]
        public String SoaEmail { get; set; }

        [JsonProperty("RETRY_SEC")]
        public int RetrySeconds { get; set; }

        [JsonProperty("MASTER_IPS")]
        public String MasterIps { get; set; }

        [JsonProperty("EXPIRE_SEC")]
        public int ExpireSeconds { get; set; }

        [JsonProperty("REFRESH_SEC")]
        public int RefreshSeconds { get; set; }

        [JsonProperty("TTL_SEC")]
        public int TTLSeconds { get; set; }

        [JsonProperty("STATUS")]
        public int StatusRaw { get; set; }
        public DomainStatusEnum Status
        {
            get
            {
                return (DomainStatusEnum)StatusRaw;
            }
        }
    }
}
