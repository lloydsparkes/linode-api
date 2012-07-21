using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.ObjectModel.NodeBalance
{
    public enum AlgorithmEnum
    {
        RoundRobin,
        LeastConn,
        Source,
        NullRecord
    }

    public enum ProtocolEnum
    {
        Http,
        Tcp,
        NullRecord
    }

    public enum StickinessEnum
    {
        None,
        Table,
        HttpCookie,
        NullRecord
    }

    public enum CheckEnum
    {
        Connection,
        Http,
        HttpBody,
        NullRecord
    }

    public class BalancerConfig
    {
        [JsonProperty("CONFIGID")]
        public int Id { get; set; }

        [JsonProperty("NODEBALANCERID")]
        public int BalancerId { get; set; }

        [JsonProperty("STICKINESS")]
        public string StickinessRaw { get; set; }
        public StickinessEnum Stickiness
        {
            get
            {
                if (string.IsNullOrEmpty(StickinessRaw))
                    return StickinessEnum.NullRecord;

                try
                {
                    return (StickinessEnum)Enum.Parse(typeof(StickinessEnum), StickinessRaw, true);
                }
                catch (Exception e)
                {
                    return StickinessEnum.NullRecord;
                }
            }
        }

        [JsonProperty("PROTOCOL")]
        public string ProtocolRaw { get; set; }
        public ProtocolEnum Protocol
        {
            get
            {
                if (string.IsNullOrEmpty(ProtocolRaw))
                    return ProtocolEnum.NullRecord;

                try
                {
                    return (ProtocolEnum)Enum.Parse(typeof(ProtocolEnum), ProtocolRaw, true);
                }
                catch (Exception e)
                {
                    return ProtocolEnum.NullRecord;
                }
            }
        }

        [JsonProperty("CHECK_PATH")]
        public String CheckPath { get; set; }

        [JsonProperty("PORT")]
        public String Port { get; set; }

        [JsonProperty("CHECK")]
        public string CheckRaw { get; set; }
        public CheckEnum Check
        {
            get
            {
                if (string.IsNullOrEmpty(CheckRaw))
                    return CheckEnum.NullRecord;

                try
                {
                    return (CheckEnum)Enum.Parse(typeof(CheckEnum), CheckRaw, true);
                }
                catch (Exception e)
                {
                    return CheckEnum.NullRecord;
                }
            }
        }

        [JsonProperty("CHECK_INTERVAL")]
        public int CheckInterval { get; set; }

        [JsonProperty("CHECK_TIMEOUT")]
        public int CheckTimeout { get; set;}

        [JsonProperty("ALGORITHM")]
        public string AlgorithmRaw { get; set; }
        public AlgorithmEnum Algorithm
        {
            get
            {
                if (string.IsNullOrEmpty(AlgorithmRaw))
                    return AlgorithmEnum.NullRecord;

                try
                {
                    return (AlgorithmEnum)Enum.Parse(typeof(AlgorithmEnum), AlgorithmRaw, true);
                }
                catch (Exception e)
                {
                    return AlgorithmEnum.NullRecord;
                }
            }
        }

        [JsonProperty("CHECK_ATTEMPTS")]
        public int CheckAttempts { get; set; }
    }
}
