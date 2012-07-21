using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Linode.Api.Linode
{
    public class Job
    {
        [JsonProperty("JOBID")]
        public int Id { get; set; }

        [JsonProperty("LINODEID")]
        public int LinodeId { get; set; }

        [JsonProperty("ACTION")]
        public String Action { get; set; }

        [JsonProperty("LABEL")]
        public String Label { get; set; }

        [JsonProperty("ENTERED_DT")]
        public DateTime Started { get; set; }

        [JsonProperty("DURATION")]
        public long Duration { get; set; }

        [JsonProperty("HOST_START_DT")]
        public DateTime HostStarted { get; set; }

        [JsonProperty("HOST_FINISH_DT")]
        public DateTime HostFinished { get; set; }

        [JsonProperty("HOST_MESSAGE")]
        public String HostMessage { get; set; }

        [JsonProperty("HOST_SUCESS")]
        public int HostSucessRaw { get; set; }
        public bool HostSucess { get { return HostSucessRaw == 1; } }
    }
}
